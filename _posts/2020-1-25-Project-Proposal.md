---
title: "Project Proposal"
excerpt_separator: "<!--more-->"
last_modified_at: 2021-01-28
categories:
  - Blog
tags:
  - Proposal
  - Background
---
Below, I have transcribed my proposal for approval from my instructor before working on this project.

## Project Overview

My proposal is to work independently on a software/research project that uses genetic algorithms to furnish a room in an optimal and aesthetic way. The user will be presented with possible solutions based on the parameters they specify. This project explores a computer's ability to creatively produce diverse solutions, allowing it to spark a human's creativity and discover ideas that did not occur to them intuitively. As part of my presentation I will analyze how well my program performs and describe how my findings and methods can be useful in other contexts.

  

## Background

-   I resumed working on my degree in 2020 after a long hiatus due to life circumstances.
-   My software engineering project involved creating webforms for a university department client in 2015.
-   CS elective courses I've taken include AI, Computer Graphics, and Evolutionary Computing(IP)

## Project Details

Evolutionary methods of problem solving have many applications, but finding an idea for my project was difficult. It seems that genetic algorithms are not yet in widespread use because of the availability of other solutions. One benefit compared to other methods is that genetic algorithms do not need a large source of data to process. I expect that they will become more prevalent as humans need to automate more of their creative work.

  

Due to my inexperience, I decided that I should start with a simple problem to prototype, and to work on adding more features as the semester progresses. I will start with only a couple pieces of furniture and develop the lower level features as I study genetic algorithms. As I understand the problem better through testing my algorithm will become more fleshed out.

  

The Unity game engine provides the ability to quickly prototype and deploy my project, allowing me to have functional builds throughout the semester. Freely available furniture models and materials will be used in order to dedicate more time to programming. My project can expand in many directions based on what challenges, progress, and suggestions I encounter.

  

## Challenges

-   Deciding if a room looks "right" is subjective, but there are some good ways to quantify this, such as how the furniture will function, maximizing the open space, complementary design, pathfinding, etc. These insights will factor into calculating the fitness of the design. Mutation and crossover strategies will help ensure a diverse set of solutions is explored.
-   As more complexity is added, the search space increases, and the algorithm will take longer to produce good results. I will need to find a solution, such as partitioning areas that can work independently.
-   Uncertainty. This is a hard problem and if the algorithm isn't designed well, even a good enough solution might not be found. In the worst case, I would have to resort to other methods to help ensure good results.
-   My project isn't entirely original, as I have found similar projects through Google [https://arxiv.org/pdf/2008.11258.pdf](https://arxiv.org/pdf/2008.11258.pdf)  and [https://publik.tuwien.ac.at/files/publik_262718.pdf](https://publik.tuwien.ac.at/files/publik_262718.pdf). My project will likely be simpler, and I will draw inspiration, but I do not want to plagiarize. One area I can differentiate my project is allowing the user to import models and export designs, but I will be looking for suggestions.

## Technical Details

-   Development environments: Unity, Visual Studio, Blender
-   PL: C#, C++
-   Source control: Git
-   Deployment: There will be a website hosted on GitHub Pages with the presentation materials, documentation, source code, and the program, which can run in the web browser window or be built for any device.


