<!--
Writing rules for every lesson:
- Visual first: every section has a diagram (Mermaid, `direction TB`), a chart (xychart-beta) or a trace table.
- Short paragraphs: if a section is only text, add a visual or a pause.
- At least two "⏸️ Pause and think" boxes, with the answer inside <details>.
- Charts need explicit colors: %%{init: {"themeVariables": {"xyChart": {"plotColorPalette": "#2563eb"}}}}%%
- Every number in the text must match the code, the tests or a real run.
- Public methods document every <param> and <returns>: what it means in the scenario, its unit, its constraints.
  The build enforces missing comments (GenerateDocumentationFile): keep it at 0 warnings.
-->

# NN — Lesson Title

| Difficulty | Module | Time |
|------------|--------|------|
| ★☆☆ Foundation / ★★☆ Intermediate / ★★★ Advanced — for two levels: `★☆☆ Foundation (sections 1–5) → ★★☆ Intermediate (sections 6–8)` | [N — Module name](../../README.md#module-n--module-name) | ~XX minutes |

**Prerequisites:** lessons or concepts the reader needs (with links).

**You'll learn:** 2–4 bullet points with the takeaways of the lesson.

---

## 1. The Problem

A real-world scenario first (why would anyone need this?), then the precise statement with input, output and constraints.

Example input → expected output.

## 2. Before You Start

The questions to ask yourself (or the interviewer) before writing any code: input size, duplicates, empty input, can values be negative...

## 3. Think First

> Try to solve it before reading on. Open the hints one at a time.

<details>
<summary>Hint 1</summary>

A small nudge.

</details>

<details>
<summary>Hint 2</summary>

A bigger nudge.

</details>

## 4. The Brute-Force Solution

The obvious approach, the code, why it's correct, and its complexity. Why it's not good enough.

## 5. The Better Idea

The key insight, explained with a vertical Mermaid diagram and/or a step-by-step trace table on a small example.

### ⏸️ Pause and think

A question that checks the reader got the insight before seeing the code.

<details>
<summary>Answer</summary>

The answer, with a short explanation.

</details>

📌 **In a nutshell:** the insight in one sentence.

## 6. Implementation

The C# code (link to the file in `src/`), walked through section by section.

## 7. Complexity

| Approach    | Time | Space | Why |
|-------------|------|-------|-----|
| Brute force |      |       |     |
| Optimized   |      |       |     |

## 8. Edge Cases and Tests

The cases that break naive solutions, and the tests that cover them (link to the file in `tests/`).

## 9. Common Mistakes

- Mistake → why it's wrong → how to avoid it.

## 10. In .NET

Built-in types or methods that already solve (part of) the problem, and when to prefer them.

## 11. Practice

1. ★ An easier variation
2. ★★ A variation that changes one constraint
3. ★★★ A harder follow-up

## 🧠 Quick Check

3–4 questions, each with the answer in a `<details>` block.

## 📌 Summary

A short vertical diagram with the key steps of the lesson, then a link to the next lesson.
