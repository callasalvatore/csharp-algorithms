# 01 — Two Sum and Its Variations

| Difficulty | Module | Time |
|------------|--------|------|
| ★☆☆ Foundation (sections 1–5) → ★★☆ Intermediate (sections 6–8) | [1 — Arrays, Hashing and Two Pointers](../../README.md#module-1--arrays-hashing-and-two-pointers) | ~40 minutes |

**Prerequisites:** [Lesson 00 — Big-O in practice](../00-big-o/README.md), `Dictionary<TKey, TValue>`, sorting.

**You'll learn:**

- how a `Dictionary` turns an O(n²) search into O(n) by **remembering what you've seen**
- the **two pointers** technique on sorted data, and *why* it never misses a solution
- how to extend the idea from 2 numbers to 3, and then to any `k`
- how to test a fast algorithm against a slow, obviously correct one

---

## 1. The Problem

You work on the accounting software of a small company. A bank transfer of **1,250.00 EUR** has just arrived, with no reference. The customer has five open invoices:

| Invoice | Amount     |
|---------|-----------:|
| INV-1   | 310.00 EUR |
| INV-2   | 475.00 EUR |
| INV-3   | 820.00 EUR |
| INV-4   | 155.00 EUR |
| INV-5   | 430.00 EUR |

**Which two invoices does the transfer pay?**

```mermaid
flowchart TB
    T["Bank transfer<br/>1,250.00 EUR"] --> Q{"Which two open invoices<br/>add up to exactly this amount?"}
    Q --> A["INV-3: 820.00 EUR"]
    Q --> B["INV-5: 430.00 EUR"]
```

**The statement:** given a list of amounts and a target, find **two different items** whose sum is exactly the target, and return their positions. If no pair exists, say so.

> [!NOTE]
> **Why amounts are in cents.** The code stores 1,250.00 EUR as the `int` `125000`. Money should never live in a `double`: in C#, `0.1 + 0.2 == 0.3` is **false**, because binary floating point can't represent those values exactly. `decimal` is exact and fine for business code; integer cents are what most payment APIs use, and what you'll see in interviews.

---

## 2. Before You Start

Questions to ask yourself (or the interviewer) before writing code:

| Question | Our answer |
|----------|-----------|
| Can the same invoice be used twice? | **No**: two *different* items |
| Can two invoices have the same amount? | **Yes**: 300 + 300 = 600 is valid with two 300s |
| Can amounts be negative? | **Yes**: credit notes |
| What if several pairs match? | Return any one of them |
| What if none matches? | Return `null` |
| Is the list sorted? | Not in general: we'll see both cases |

---

## 3. Think First

> Try to solve it before reading on. Open the hints one at a time.

<details>
<summary>Hint 1</summary>

Pick one invoice, say 820.00. Which amount would you need to find to reach 1,250.00?

</details>

<details>
<summary>Hint 2</summary>

You need `target - amount`. So the question becomes: *"is that missing amount somewhere in the list?"* Which data structure answers "is X in here?" in O(1)?

</details>

---

## 4. The Brute-Force Solution

Try every pair. It's the same shape as the duplicate check in lesson 00:

```csharp
for (var i = 0; i < amounts.Count; i++)
    for (var j = i + 1; j < amounts.Count; j++)
        if ((long)amounts[i] + amounts[j] == target)
            return new PairResult(i, j);
```

It's correct and simple. But with `n` invoices it checks up to **n × (n − 1) / 2 pairs**: that's **O(n²)**.

| Open invoices | Pairs to check |
|--------------:|---------------:|
| 10            | 45             |
| 1,000         | 499,500        |
| 100,000       | ~5 billion     |

A company with 100,000 open invoices would wait seconds for every single transfer.

---

## 5. The Better Idea: Remember What You've Seen

Walk through the invoices **once**. For each amount, compute the **missing amount** (`target − amount`) and ask a dictionary: *"have I already seen it?"*

```mermaid
flowchart TB
    S{"Any amounts left?"} -->|yes| M["Take the next amount<br/>missing = target − amount"]
    S -->|no| N["❌ No pair"]
    M --> Q{"Is 'missing' in<br/>the dictionary?"}
    Q -->|yes| F["✅ Found the pair:<br/>(index of missing, current index)"]
    Q -->|no| R["Remember this amount<br/>and its index"]
    R --> S
```

Here's the trace for the 1,250.00 EUR transfer:

| Step | Amount | Missing  | Seen so far                   | Result |
|-----:|-------:|---------:|-------------------------------|--------|
| 1    | 310.00 | 940.00   | —                             | not seen, remember 310.00 |
| 2    | 475.00 | 775.00   | 310                           | not seen, remember 475.00 |
| 3    | 820.00 | 430.00   | 310, 475                      | not seen, remember 820.00 |
| 4    | 155.00 | 1,095.00 | 310, 475, 820                 | not seen, remember 155.00 |
| 5    | 430.00 | **820.00** | 310, 475, **820**, 155      | ✅ **INV-3 + INV-5** |

One pass, one dictionary lookup per invoice: **O(n)** time, **O(n)** memory for the dictionary.

### ⏸️ Pause and think

In the loop, we **first** look up the missing amount, **then** add the current one to the dictionary. What would go wrong if we did it the other way around?

<details>
<summary>Answer</summary>

An item could be paired **with itself**. With a single invoice of 300.00 and a target of 600.00, we'd add 300.00, then look for 300.00 and find it: a wrong match. Checking first guarantees that the dictionary only contains *other* items.

</details>

### How big is the difference?

```mermaid
%%{init: {"themeVariables": {"xyChart": {"plotColorPalette": "#dc2626, #2563eb"}}}}%%
xychart-beta
    title "Worst-case steps: brute force (red) vs dictionary (blue)"
    x-axis "Invoices (n)" [2, 3, 4, 5, 6, 7, 8, 9, 10]
    y-axis "Steps" 0 --> 50
    line [1, 3, 6, 10, 15, 21, 28, 36, 45]
    line [2, 3, 4, 5, 6, 7, 8, 9, 10]
```

The [tests](../../tests/Algorithms.Tests/Arrays/TwoSumTests.cs) check the exact numbers: with 1,000 invoices and no match, the brute force does **499,500** steps, the dictionary **1,000**.

📌 **In a nutshell:** instead of *searching* for the partner of each item, *remember* the items you've seen and *look up* the partner in O(1).

---

## 6. Variation: The List Is Already Sorted

Suppose the invoices come from a database query with `ORDER BY amount`. Can we do better than O(n) memory?

Yes, with **two pointers**: one on the smallest amount, one on the largest.

```mermaid
flowchart TB
    S["left = smallest, right = largest"] --> C{"amount[left] + amount[right]"}
    C -->|"= target"| F["✅ Found"]
    C -->|"< target"| L["Too small:<br/>move left one step right"]
    C -->|"> target"| R["Too big:<br/>move right one step left"]
    L --> E{"left < right?"}
    R --> E
    E -->|yes| C
    E -->|no| N["❌ No pair"]
```

Trace with the sorted invoices and a transfer of **905.00 EUR**:

| Step | Left   | Right  | Sum    | Decision |
|-----:|-------:|-------:|-------:|----------|
| 1    | 155.00 | 820.00 | 975.00 | too big → move **right** |
| 2    | 155.00 | 475.00 | 630.00 | too small → move **left** |
| 3    | 310.00 | 475.00 | 785.00 | too small → move **left** |
| 4    | 430.00 | 475.00 | 905.00 | ✅ found |

Each step discards one item, so there are at most `n − 1` steps: **O(n)** time, **O(1)** memory.

### ⏸️ Pause and think

At step 1, the sum is too big and we discard 820.00 for good. How can we be sure that 820.00 isn't part of the answer with some *other* invoice?

<details>
<summary>Answer</summary>

820.00 was paired with **155.00, the smallest amount still available**, and the sum was already too big. Any other partner is larger than 155.00, so the sum would be even bigger. 820.00 can't be in the answer, so it's safe to drop it.

The same reasoning works the other way: if the sum is too small with the **largest** partner available, the left item can't reach the target with anyone.

</details>

```mermaid
flowchart TB
    A["Sum too big with the SMALLEST partner"] --> B["Every other partner is bigger"]
    B --> C["The sum can only get bigger"]
    C --> D["The right item is useless: discard it"]
```

📌 **In a nutshell:** on sorted data, every comparison tells you which item can **never** be part of the answer, so you can throw it away.

> [!WARNING]
> Two pointers returns positions **in the sorted list**. If the input isn't sorted and you need the original positions, sorting costs O(n log n) and you must remember where each item came from. In that case, the dictionary is simpler and faster.

---

## 7. From Two to Three: 3Sum

New request from the accounting team: in the ledger, find **every group of three entries that balance to zero** (e.g. two refunds and one charge).

```
Ledger entries (EUR): -100, 0, 100, 200, -100, -400
```

The idea: **sort**, then **fix the first entry** and search the other two with two pointers, exactly as before.

```mermaid
flowchart TB
    A["Sort: -400, -100, -100, 0, 100, 200"] --> B["Fix entry i"]
    B --> C["Two pointers on the entries after i,<br/>looking for sum = 0 − entry i"]
    C --> D["Next i<br/>(skip it if it's equal to the previous one)"]
    D --> B
```

Result: **(−100, −100, 200)** and **(−100, 0, 100)**.

For each of the `n` fixed entries we run an O(n) two-pointer search: **O(n²)** in total, instead of O(n³) for trying every triple.

### ⏸️ Pause and think

The sorted list contains −100 **twice**. Why does the code skip the second −100 when it's the fixed entry?

<details>
<summary>Answer</summary>

Fixing the first −100 already finds every triple that starts with −100. Fixing the second one would find **(−100, 0, 100)** again: a duplicate. Skipping equal values (for the fixed entry *and* for the two pointers after a match) is what makes every triple appear only once.

</details>

The code is in [ThreeSum.cs](../../src/Algorithms/Arrays/ThreeSum.cs).

---

## 8. Any Number: k-Sum

The same trick works for any `k`: fix one value, and solve the problem for `k − 1` values on the rest. Repeat until only two are left, then use two pointers.

```mermaid
flowchart TB
    K4["4Sum: fix one value"] --> K3["3Sum on the rest: fix one value"]
    K3 --> K2["2Sum on the rest: two pointers"]
```

Every level adds a loop over `n` values, so k-Sum costs **O(n^(k−1))**: O(n) for 2Sum on sorted data, O(n²) for 3Sum, O(n³) for 4Sum. See [KSum.cs](../../src/Algorithms/Arrays/KSum.cs): the recursion and the duplicate skipping are the same ideas as 3Sum, written once for every `k`.

---

## 9. Complexity

| Approach                         | Time          | Extra space | Why |
|----------------------------------|---------------|-------------|-----|
| Two Sum, brute force             | O(n²)         | O(1)        | tries every pair |
| Two Sum, dictionary              | O(n)          | O(n)        | one pass, O(1) lookup per item |
| Two Sum, two pointers (sorted)   | O(n)          | O(1)        | each step discards one item |
| 3Sum, sort + two pointers        | O(n²)         | O(n)        | n fixed values × O(n) search, plus a sorted copy |
| k-Sum, sort + recursion          | O(n^(k−1))    | O(n)        | one loop per extra value |

---

## 10. Edge Cases and Tests

All of these are covered in the [tests](../../tests/Algorithms.Tests/Arrays/):

| Case | Example | Expected |
|------|---------|----------|
| Two equal amounts | `[300, 300]`, target 600 | found |
| Same item twice | `[300, 500]`, target 600 | **not** found |
| No match, empty list, one item | `[]`, `[100]` | `null` |
| Negative amounts (credit notes) | `[-2000, 15000, 7000]`, target 5000 | found |
| Overflow | `[int.MaxValue, int.MaxValue]`, target −2 | **not** found |
| Repeated values in 3Sum | `[0, 0, 0, 0, 0]`, target 0 | `(0, 0, 0)` only once |

### 🧪 A testing technique worth stealing

How do you know the fast version is right on inputs you didn't think of? **Compare it with the brute force** on hundreds of random inputs. The brute force is slow but obviously correct, so it acts as a referee:

```mermaid
flowchart TB
    R["Random input<br/>(fixed seed, so failures are reproducible)"] --> B["Brute force"]
    R --> F["Fast version"]
    B --> C{"Same answer?"}
    F --> C
    C -->|"500 times yes"| OK["✅ Confident"]
    C -->|"once no"| BUG["🐞 Bug found,<br/>with the input that causes it"]
```

The tests do this for Two Sum, 3Sum and 4Sum.

---

## 11. Common Mistakes

| Mistake | What happens | Fix |
|---------|--------------|-----|
| Adding the current item to the dictionary **before** checking | An item pairs with itself: `[300]` "solves" target 600 | Check first, then add |
| Summing two `int`s | `int.MaxValue + int.MaxValue` wraps to −2: a false match | Cast to `long` before adding |
| Using two pointers on unsorted data | It skips valid pairs | Sort first, or use the dictionary |
| Returning indices of the sorted copy | Positions don't match the original list | Use the dictionary, or keep the original indices |
| Not skipping duplicates in 3Sum | The same triple is returned twice | Skip equal values for the fixed item and after a match |

---

## 12. In .NET

- **`Dictionary.TryGetValue`** does the lookup and returns the value in **one** search. `ContainsKey` followed by `dict[key]` searches twice.
- **`Dictionary.TryAdd`** adds only if the key is missing: here it keeps the **first** index when an amount appears twice.
- **`HashSet<T>`** is enough when you only need *"does a pair exist?"* and not the positions.
- **`Enumerable.Order()`** (.NET 7+) and **`Array.Sort`** sort in O(n log n).

---

## 13. Practice

1. ★ Return the **invoice codes** (`INV-3`, `INV-5`) instead of the indices, using a `record Invoice(string Code, int AmountInCents)`.
2. ★★ **Count** how many different pairs add up to the target. With `[300, 300, 300]` and target 600 the answer is 3.
3. ★★ **Closest sum**: on sorted data, find the pair whose sum is closest to the target when no exact match exists. *(Hint: two pointers, remember the best sum seen.)*
4. ★★★ **4Sum in O(n²)** on average: store the sums of all pairs in a dictionary. What makes avoiding duplicates hard with this approach?

---

## 🧠 Quick Check

**1.** The list is unsorted, you need the original positions, and memory isn't a problem. Which approach?

<details>
<summary>Answer</summary>

The **dictionary**: O(n) time, and it works directly on the original positions.

</details>

**2.** The list is sorted, it has 100 million items, and you're short on memory. Which approach?

<details>
<summary>Answer</summary>

**Two pointers**: O(n) time like the dictionary, but O(1) extra memory instead of a dictionary with up to 100 million entries.

</details>

**3.** What's the complexity of 5Sum with the method of section 8?

<details>
<summary>Answer</summary>

**O(n⁴)**: k − 1 = 4 nested levels of work over `n` values.

</details>

**4.** Why does the code compute `(long)amounts[i] + amounts[j]` instead of `amounts[i] + amounts[j]`?

<details>
<summary>Answer</summary>

Two large `int`s can **overflow** and wrap around to a completely different number, which could even match the target by accident. Converting to `long` first makes the sum exact.

</details>

---

## 📌 Summary

```mermaid
flowchart TB
    A["Unsorted data"] --> B["Dictionary:<br/>remember what you've seen<br/>O(n) time, O(n) memory"]
    C["Sorted data"] --> D["Two pointers:<br/>discard what can't be in the answer<br/>O(n) time, O(1) memory"]
    D --> E["3Sum, k-Sum:<br/>fix one value, reduce to 2Sum<br/>O(n^(k−1))"]
```

**Next:** [Lesson 02 — Maximum Subarray Sum](../../README.md#-syllabus), where a single pass with the right running value solves a problem that looks like it needs every subarray.
