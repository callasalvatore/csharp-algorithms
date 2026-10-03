# C# Algorithms — A Hands-On Course

A practical course on **data structures and algorithms in C#**, built around 16 classic problems that show up in technical interviews and in real backend code.

Every lesson teaches *how to think* about the problem, not just the final answer: we start from a concrete scenario, try the obvious solution, measure why it's not enough, and improve it step by step. Each solution comes with **xUnit tests** you can run, break and extend.

> [!NOTE]
> The course is being written one lesson at a time. Lessons marked 📝 are planned; ✅ means ready.

---

## 🎯 Difficulty at a Glance

| Level | Meaning | Lessons |
|-------|---------|---------|
| ★☆☆ **Foundation** | One core idea, applied directly. A good starting point. | 4 |
| ★★☆ **Intermediate** | Combines two ideas, or needs a data structure you have to choose carefully. | 10 |
| ★★★ **Advanced** | Non-obvious insight or tricky invariants. Expect to re-read and experiment. | 3 |

Some lessons span two levels: the basic version is easier, the follow-up variations are harder (e.g. LRU is ★★☆, LFU is ★★★). They're counted at their starting level.

---

## 👤 Who This Course Is For

**You should already be comfortable with:**

- C# fundamentals: classes, interfaces, generics, properties, `record`
- The everyday collections: `List<T>`, `Dictionary<TKey, TValue>`, `HashSet<T>`, `Queue<T>`, `Stack<T>`
- Basic LINQ (`Where`, `Select`, `OrderBy`)
- Recursion: you can read and write a simple recursive function
- Running a project from the command line (`dotnet build`, `dotnet test`)

**You don't need:**

- A computer science degree
- Prior knowledge of heaps, graphs, tries or dynamic programming: each one is introduced when it's first needed
- Experience with Big-O notation: [Lesson 00](#-syllabus) explains everything the course uses

**Typical reader:** a developer with 1–3 years of experience who wants solid fundamentals or is preparing for technical interviews. More experienced developers can jump straight to the ★★★ lessons and the variations.

---

## 🧭 How Each Lesson Works

Every lesson follows the same path, the way a teacher would walk you through it at the whiteboard:

| Step | What you'll find |
|------|------------------|
| 1. **The problem** | A real-world scenario (a cache in front of a slow API, a task queue, a search box...) and a precise statement |
| 2. **Before you start** | Prerequisites, and the key questions to ask yourself before writing code |
| 3. **Think first** | Progressive hints, hidden in collapsible sections: open them one at a time only if you're stuck |
| 4. **The brute-force solution** | The obvious approach, why it works, and why it's too slow or too memory-hungry |
| 5. **The better idea** | The insight that changes everything, explained with diagrams and step-by-step traces |
| 6. **Implementation** | The C# code, walked through line by line |
| 7. **Complexity** | Time and space, with the reasoning behind them, not just the formula |
| 8. **Edge cases and tests** | What can go wrong, and the tests that prove it doesn't |
| 9. **Common mistakes** | The bugs people actually write in interviews and in production |
| 10. **In .NET** | What the framework already gives you (`PriorityQueue`, `SortedSet`, `LinkedList`...) and when to use it |
| 11. **Practice** | Variations to try on your own, from easier to harder |

Lessons are **visual first**: every idea comes with a diagram, a chart or a step-by-step trace, and short paragraphs instead of long pages of text. Along the way you'll find:

- ⏸️ **Pause and think**: a question to answer before reading on, with the answer hidden until you open it
- 📌 **In a nutshell**: a one-line recap of what you just learned
- 🧠 **Quick check**: a short quiz at the end of the lesson
- 🧪 **Try it**: runnable experiments that let you see the theory in action

### How to study

1. **Read the problem and stop.** Try to solve it on paper or in code before reading further, even for 15 minutes.
2. **Use the hints one at a time.** Each hint gives away a bit more.
3. **Read the solution, then close it and rewrite it from memory.** That's when you find out what you really understood.
4. **Run the tests, then break the code on purpose** and watch which tests fail. It's the fastest way to understand why each line is there.

---

## 📚 Syllabus

The lessons are ordered so that each one only uses ideas from the previous ones.

### Module 0 — Foundations

| # | Lesson | Difficulty | Key ideas | Status |
|---|--------|------------|-----------|--------|
| 00 | [How to measure an algorithm: Big-O in practice](lessons/00-big-o/README.md) | ★☆☆ | counting steps, the 6 common complexities, time vs space, amortized cost | ✅ |

### Module 1 — Arrays, Hashing and Two Pointers

| # | Lesson | Difficulty | Key ideas | Status |
|---|--------|------------|-----------|--------|
| 01 | Two Sum and its variations | ★☆☆ → ★★☆ | hash map, sorted array + two pointers, k-sum | 📝 |
| 02 | Maximum Subarray Sum | ★☆☆ | Kadane's algorithm, variations (indices, circular, product) | 📝 |
| 03 | Trapping Rain Water | ★★★ | prefix max, two pointers, monotonic stack | 📝 |

### Module 2 — Binary Search

| # | Lesson | Difficulty | Key ideas | Status |
|---|--------|------------|-----------|--------|
| 04 | Binary Search on the Answer | ★★☆ | monotonic predicates: capacity, days, weights | 📝 |

### Module 3 — Heaps and Priority Queues

| # | Lesson | Difficulty | Key ideas | Status |
|---|--------|------------|-----------|--------|
| 05 | Kth Largest Element in a Stream | ★☆☆ | min-heap of size k | 📝 |
| 06 | Top K Frequent Elements | ★★☆ | counting + heap, bucket sort | 📝 |
| 07 | Merge K Sorted Lists | ★★☆ | k-way merge with a heap | 📝 |
| 08 | Find Median from Data Stream | ★★★ | two balanced heaps | 📝 |
| 09 | A Task Scheduler with Priorities | ★★☆ | priority queue design, tie-breaking, starvation | 📝 |

### Module 4 — Designing Data Structures

| # | Lesson | Difficulty | Key ideas | Status |
|---|--------|------------|-----------|--------|
| 10 | LRU and LFU Cache | ★★☆ → ★★★ | hash map + doubly linked list, frequency buckets | 📝 |

### Module 5 — Trees and Graphs

| # | Lesson | Difficulty | Key ideas | Status |
|---|--------|------------|-----------|--------|
| 11 | Serialize and Deserialize a Binary Tree | ★★☆ | pre-order traversal, null markers | 📝 |
| 12 | Clone a Graph with Random Pointers | ★★☆ | old → new node map, DFS/BFS | 📝 |
| 13 | Detect a Cycle in a Directed Graph | ★★☆ | DFS with three colors, Kahn's topological sort | 📝 |
| 14 | Word Ladder | ★★★ | BFS shortest path, implicit graphs, bidirectional BFS | 📝 |

### Module 6 — Dynamic Programming

| # | Lesson | Difficulty | Key ideas | Status |
|---|--------|------------|-----------|--------|
| 15 | Longest Increasing Subsequence | ★★☆ → ★★★ | O(n²) DP, patience sorting in O(n log n) | 📝 |
| 16 | Word Break | ★★☆ | DP on prefixes, trie to speed up lookups | 📝 |

---

## 🗂️ Repository Structure

```
csharp-algorithms/
├── lessons/                  one folder per lesson, with its README (the lesson itself)
├── src/Algorithms/           the solutions, one folder per module
├── tests/Algorithms.Tests/   xUnit tests, mirroring the src structure
└── experiments/              small runnable programs used by the "Try it" sections
```

## ✅ Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Any IDE (e.g., [Visual Studio](https://visualstudio.microsoft.com/), [Rider](https://www.jetbrains.com/rider/), or [VS Code](https://code.visualstudio.com/))

## 🚀 Getting Started

```bash
git clone https://github.com/callasalvatore/csharp-algorithms.git
cd csharp-algorithms
dotnet test
```

To run only the tests of one module (e.g. the Foundations module of lesson 00):

```bash
dotnet test --filter "FullyQualifiedName~Foundations"
```

## 🔗 Related

- [C# Design Patterns](https://github.com/callasalvatore/csharp-design-patterns): the 23 GoF patterns with concrete examples.

Contributions and suggestions are welcome.
