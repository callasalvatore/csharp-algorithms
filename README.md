# C# Algorithms: a hands-on course

A practical course on data structures and algorithms in C#, built around 16 classic problems that come up in technical interviews and in real backend code.

Each lesson shows how to think about a problem, not just the final answer. We start from a concrete situation, try the obvious solution, see why it isn't good enough and improve it step by step. Every solution comes with xUnit tests that you can run, break and extend.

The course is being written one lesson at a time: the syllabus below shows which lessons are ready.

## Difficulty

Every lesson has one of three levels:

| Level | Meaning | Lessons |
|-------|---------|---------|
| ★☆☆ Foundation | One core idea, applied directly. A good place to start. | 4 |
| ★★☆ Intermediate | Combines two ideas, or needs a data structure you have to choose carefully. | 10 |
| ★★★ Advanced | Needs a non-obvious insight. Expect to re-read and experiment. | 3 |

Some lessons span two levels, written as ★☆☆ → ★★☆. The first sections are at the starting level, the later ones (variations and follow-ups) at the higher one. Each lesson says which sections belong to which level, so you can stop after the first part and come back later. In the table above, these lessons are counted at their starting level.

## Who this course is for

You should already be comfortable with C# basics (classes, interfaces, generics, records), with the everyday collections (`List<T>`, `Dictionary<TKey, TValue>`, `HashSet<T>`, `Queue<T>`, `Stack<T>`), with simple LINQ queries and with recursion. You should also know how to run `dotnet build` and `dotnet test`.

You don't need a computer science degree, and you don't need to know heaps, graphs, tries or dynamic programming: each topic is introduced when it's first needed. Big-O notation isn't required either, since lesson 00 explains everything the course uses.

The course is aimed at developers with one to three years of experience who want solid fundamentals or are preparing for technical interviews. If you have more experience, you can jump straight to the ★★★ lessons and to the variations.

## How each lesson works

Every lesson follows the same path, the way I'd explain it at a whiteboard:

1. The problem: a real situation and a precise statement.
2. Before you start: the questions to ask before writing any code.
3. Think first: the exercise to try on your own, with hints you can open one at a time.
4. The brute-force solution, and why it isn't enough.
5. The better idea, explained with diagrams and step-by-step traces.
6. The implementation, walked through section by section.
7. Complexity, with the reasoning behind it.
8. Edge cases, and the tests that cover them.
9. Common mistakes.
10. What .NET already gives you.
11. Practice: variations to try on your own.

The lessons rely on diagrams and charts rather than long pages of text. Along the way you'll find short "Stop and think" questions with the answer hidden until you open it, a quick quiz at the end, and small programs in `experiments/` that let you see the theory in action.

### How to study

Read the problem and stop there. Try to solve it, on paper or in code, even for fifteen minutes. If you get stuck, open the hints one at a time, since each one gives away a little more.

After reading the solution, close it and rewrite it from memory: that's when you find out what you really understood. Then run the tests and break the code on purpose to see which tests fail. It's the quickest way to understand why each line is there.

## Syllabus

The lessons are ordered so that each one only uses ideas from the previous ones.

### Module 0: Foundations

| # | Lesson | Difficulty | Key ideas | Status |
|---|--------|------------|-----------|--------|
| 00 | [How to measure an algorithm: Big-O in practice](lessons/00-big-o/README.md) | ★☆☆ | counting steps, the 6 common complexities, time vs space, amortized cost | Ready |

### Module 1: Arrays, hashing and two pointers

| # | Lesson | Difficulty | Key ideas | Status |
|---|--------|------------|-----------|--------|
| 01 | [Two Sum and its variations](lessons/01-two-sum/README.md) | ★☆☆ → ★★☆ | hash map, sorted array and two pointers, k-sum | Ready |
| 02 | Maximum subarray sum | ★☆☆ | Kadane's algorithm, variations (indices, circular, product) | Planned |
| 03 | Trapping rain water | ★★★ | prefix max, two pointers, monotonic stack | Planned |

### Module 2: Binary search

| # | Lesson | Difficulty | Key ideas | Status |
|---|--------|------------|-----------|--------|
| 04 | Binary search on the answer | ★★☆ | monotonic predicates: capacity, days, weights | Planned |

### Module 3: Heaps and priority queues

| # | Lesson | Difficulty | Key ideas | Status |
|---|--------|------------|-----------|--------|
| 05 | Kth largest element in a stream | ★☆☆ | min-heap of size k | Planned |
| 06 | Top K frequent elements | ★★☆ | counting and heap, bucket sort | Planned |
| 07 | Merge K sorted lists | ★★☆ | k-way merge with a heap | Planned |
| 08 | Find the median from a data stream | ★★★ | two balanced heaps | Planned |
| 09 | A task scheduler with priorities | ★★☆ | priority queue design, tie-breaking, starvation | Planned |

### Module 4: Designing data structures

| # | Lesson | Difficulty | Key ideas | Status |
|---|--------|------------|-----------|--------|
| 10 | LRU and LFU cache | ★★☆ → ★★★ | hash map with a doubly linked list, frequency buckets | Planned |

### Module 5: Trees and graphs

| # | Lesson | Difficulty | Key ideas | Status |
|---|--------|------------|-----------|--------|
| 11 | Serialize and deserialize a binary tree | ★★☆ | pre-order traversal, null markers | Planned |
| 12 | Clone a graph with random pointers | ★★☆ | old-to-new node map, DFS and BFS | Planned |
| 13 | Detect a cycle in a directed graph | ★★☆ | DFS with three colors, Kahn's topological sort | Planned |
| 14 | Word ladder | ★★★ | BFS shortest path, implicit graphs, bidirectional BFS | Planned |

### Module 6: Dynamic programming

| # | Lesson | Difficulty | Key ideas | Status |
|---|--------|------------|-----------|--------|
| 15 | Longest increasing subsequence | ★★☆ → ★★★ | O(n²) DP, patience sorting in O(n log n) | Planned |
| 16 | Word break | ★★☆ | DP on prefixes, a trie to speed up lookups | Planned |

## Appendix

Some topics come up in several lessons without being algorithms themselves. They live in the appendix, which you can read when a lesson points to it or on its own.

| | Topic | Status |
|---|-------|--------|
| A | [Money in code](appendix/A-money-in-code.md): `double` vs `decimal` vs integer cents, rounding, splitting amounts | Ready |
| B | How a dictionary works: hashing, collisions, why lookups are O(1) on average | Planned |
| C | Integer overflow: how an `int` wraps around, `checked`, when to use `long` | Planned |
| D | Testing against a brute force: slow but correct referees, random inputs | Planned |
| E | [How sorting works](appendix/E-how-sorting-works.md): insertion sort, merge sort, what .NET uses, stability | Ready |

The [appendix index](appendix/README.md) shows which lessons use each topic.

## Repository structure

```
csharp-algorithms/
├── lessons/                  one folder per lesson, with its README (the lesson itself)
├── appendix/                 topics shared by several lessons
├── src/Algorithms/           the solutions, one folder per module
├── tests/Algorithms.Tests/   xUnit tests, mirroring the src structure
└── experiments/              small runnable programs used by the lessons
```

## Getting started

You need the [.NET 10 SDK](https://dotnet.microsoft.com/download) and any editor or IDE, for example [Visual Studio](https://visualstudio.microsoft.com/), [Rider](https://www.jetbrains.com/rider/) or [VS Code](https://code.visualstudio.com/).

```bash
git clone https://github.com/callasalvatore/csharp-algorithms.git
cd csharp-algorithms
dotnet test
```

To run only the tests of one module, for example the Foundations module of lesson 00:

```bash
dotnet test --filter "FullyQualifiedName~Foundations"
```

## Related

If you're interested in design patterns, [C# Design Patterns](https://github.com/callasalvatore/csharp-design-patterns) covers the 23 GoF patterns with concrete examples.

Contributions and suggestions are welcome.
