<!--
Writing rules for every lesson.

Content:
- Visual first: every section has a diagram (Mermaid, `direction TB`), a chart (xychart-beta) or a trace table.
- At least two "Stop and think" questions, with the answer inside <details>.
- Charts need explicit colors: %%{init: {"themeVariables": {"xyChart": {"plotColorPalette": "#2563eb"}}}}%%
- Every number in the text must match the code, the tests or a real run.
- Side topics that aren't the algorithm (money types, hashing, overflow...) go in the appendix: keep a 2 or 3
  sentence explanation in the lesson, with a link to the appendix.
- Never take anything for granted. When something surprising happens (a number that prints oddly, a lookup
  that fails), explain the mechanism step by step, down to the level where the reader can check it alone.
  Every step must follow from the previous one: no "so" or "therefore" without the reasoning that leads there.
  If a topic needs basics the reader may not have, put them in an earlier appendix and link to it.
- Use real values, computed by code in the repo (and checked by tests), never values written from memory.
- Public methods document every <param> and <returns>: what it means in the scenario, its unit, its constraints.
  The build enforces missing comments (GenerateDocumentationFile): keep it at 0 warnings.

Style (plain and clear, like a colleague explaining at a whiteboard):
- Short paragraphs of plain prose. Use a list only for real sequences of steps or real enumerations.
- Tables only for data: traces, complexities, inputs and outputs, edge cases.
- No em dashes, no emoji (also inside diagrams), no GitHub alert boxes.
- Headings in sentence case: "## 5. A better idea", not "## 5. A Better Idea".
- Bold only for a key term the first time it appears, if at all.
- Plain "-" for minus, and words instead of arrows in sentences. The difficulty notation ★☆☆ → ★★☆ is the only arrow.
-->

# Lesson NN: Title

| Difficulty | Module | Time |
|------------|--------|------|
| ★☆☆ Foundation, ★★☆ Intermediate or ★★★ Advanced. For two levels: ★☆☆ Foundation (sections 1 to 5) → ★★☆ Intermediate (sections 6 to 8) | [Module N: Name](../../README.md#module-n-name) | about XX minutes |

**Prerequisites:** lessons or concepts the reader needs, with links.

One short paragraph with what the reader will learn.

## 1. The problem

A real-world situation first (why would anyone need this?), then the precise statement with input, output and constraints.

## 2. Before you start

The questions to ask yourself (or the interviewer) before writing any code, with the answers for this problem: input size, duplicates, empty input, negative values...

## 3. Think first

Restate the exercise so it can be done without scrolling back: what to write, the method signature with a comment on each parameter (meaning and unit), and a table of inputs with the expected outputs, including an edge case.

Then the goals, from "any correct solution" to the target complexity, and the hints:

<details>
<summary>Hint 1</summary>

A small nudge.

</details>

<details>
<summary>Hint 2</summary>

A bigger nudge.

</details>

## 4. The brute-force solution

The obvious approach, the code, why it's correct and what it costs. Why it's not good enough.

## 5. A better idea

The key insight, explained with a vertical Mermaid diagram and a step-by-step trace table on a small example.

### Stop and think

A question that checks the reader got the insight before seeing the code.

<details>
<summary>Answer</summary>

The answer, with a short explanation.

</details>

In short: the insight in one sentence.

## 6. Implementation

The C# code (link to the file in `src/`), walked through section by section.

## 7. Complexity

| Approach    | Time | Space | Why |
|-------------|------|-------|-----|
| Brute force |      |       |     |
| Optimized   |      |       |     |

## 8. Edge cases and tests

The cases that break naive solutions, and the tests that cover them (link to the file in `tests/`).

## 9. Common mistakes

| Mistake | What happens | Fix |
|---------|--------------|-----|
|         |              |     |

## 10. In .NET

Built-in types or methods that already solve the problem, or part of it, and when to prefer them.

## 11. Practice

1. ★ An easier variation
2. ★★ A variation that changes one constraint
3. ★★★ A harder follow-up

## Quick check

Three or four questions, each with the answer in a `<details>` block.

## Summary

A short vertical diagram with the key steps of the lesson, then a link to the next lesson.
