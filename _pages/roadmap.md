---
permalink: /roadmap/
title: "Roadmap"
---
The roadmap for this project specifies three key phases with bullet points indicating desired features to be worked on.

## Phase One - Room Prototype
The goal of this phase is to develop a foundation for the project and identify and implement key functionality. This phase will demonstrate the ability of genetic algorithms to arrange furniture.

 - [x] Represent furniture objects in a 3D space.
 - [x] Population is seeded with furniture having random position and rotation within the room.
 - [x] Collision detection and resolution between placed objects.
 - [ ] Different "types"(tags) of furniture i.e. bed, chair, table.
 - [ ] A front facing or functional side for each "type" of furniture.
 - [x] A basic genetic algorithm implementation with a function to determine room fitness.
 - [x] Rooms have walls. 
 - [ ] Rooms have doors.
 - [ ] Furniture must have a clear path to their functional side.
 - [ ] Furniture interactions i.e. chairs near tables, chairs face objects of interest.
 - [ ] The available open space should be maximized. (furniture grouped up or along walls)
 - [ ] Furniture that is aligned with other furniture on their axis is beneficial.

## Phase Two - User Interaction
The goal of phase two is to allow user interaction to determine parameters like room size and furniture selection as well as exporting the result as an image, floor plan, or format for other 3D applications.

 - [ ] User can run and view a number of solutions interactively.
 - [ ] Save/load/reset.
 - [ ] Pause/continue.
 - [ ] User can scale the floor plan and specify which furniture to use.
 - [ ] User Interface with buttons, previews, and multiple scenes.
 - [ ] Export the result.
 - [ ] Algorithm analytics and testing display.
 - [ ] Modify algorithm interactively.

## Phase Three - Feature Expansion
The final phase implements additional functionality while testing and refining.

 - [ ] Additional furniture types: decorative, lighting.
 - [ ] Rooms that are not rectangular.
 - [ ] Multiple rooms.
 - [ ] Wall/ceiling fixtures.
 - [ ] Import and tag models/materials.
 - [ ] Automatic material/color selection
 - [ ] Suggested features

### Timeline

Each phase will ideally last 1 month, but I would consider the completion of each phase an individual success if significant roadblocks occur. Documentation and testing will be done during all phases.
