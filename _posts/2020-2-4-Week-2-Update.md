---
title: "Week 2 Update"
excerpt_separator: "<!--more-->"
last_modified_at: 2021-01-28
categories:
  - Blog
tags:
  - Weekly Update
  - Update
  - Refixture
  - GitHub
  - Roadmap
---
This week, I have worked on the implementation of the 3D environment as well as implementing a basic genetic algorithm.

## Roadmap
Last week I had a basic scene with a few furniture arranged around the floor. Since then I have created a basic square room with four walls to serve as a test candidate. These rooms are tileable and have become the members of the population to be used with the basic genetic algorithm I've implemented. I've also expanded the furniture selection available, and the list of the furniture residing in the room become that room's genetic make-up.

## First steps
To set up the environment I first started making a SceneController script to make copies of the room and arrange them side-by-side in rows and columns. Then, each room had a couple scripts. RoomController is able to hide the walls for the camera to look through. And FurnitureController, whose role is in creating and positioning the furniture. With this, each room is able to act as a candidate with access to the furniture residing in it.

## Genetic Algorithm 3D
My first pass at implementing the algorithm was within the 3D space. Because the population is seeded with the furniture in random areas of the room, collisions were inevitable and common. My solution was to have each piece of furniture detect and resolve its own collisions, and have the algorithm proceed when everything was settled. This presented a problem of the program taking a long time to find suitable positions for the furniture, so if a piece couldn't find a spot, it would not be placed in the scene in an active state. I programmed the fitness function for each room to scale based on how many pieces were active in the room. The parents for the next generation were then chosen by roulette wheel selection based on their fitness. If crossover occured, they would swap the position and rotation of furniture between to parents with a chance based on the crossover rate. A mutation would mean that a piece of furniture would take a new random position. 

## Genetic Algorithm 2D
Despite the rooms being 3D, the information stored about the furniture only requires two dimensions, so I decided to take another shot at the algorithm. I created a 2D scene that will run the algoritmn in the background. Since the scale is the same it required a simple translation of the coordinates to display the rooms in the 3D scene. In the 2D scene, furniture is represented as boxes that have the same horiontal dimensions of the 3D furniture. Instead of resolving the collisions through scripts, I let the physics engine do the job, collided boxes push away from each other for a split second each generation. This allows the algorithm to move through generations much faster. The fitness function favors rooms that have each furniture touching the wall, as it appears more natural. Mutations have a chance to move the furniture or change its rotation. Below, you can see the efforts of the 2D algorithm generating the coordinates for furniture in the 3D scene. 

![Room Population](/assets/images/Unity/Unity_2021-02-04_15-06-41.png)

## Next Steps
The genetic algorithm is still in the prototyping stages so the next priority is developing new functions for evaluating the fitness. For example, furniture like a sofa shouldn't face a wall which the current system does nothing to correct.
