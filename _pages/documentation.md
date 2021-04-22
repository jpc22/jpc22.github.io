---
permalink: /documentation/
title: "Documentation Overview"
toc: true
toc_label: "Documentation"
toc_icon: "bars"
toc_sticky: true
---

This documentation is split into workflow and genetic algorithm sections. Workflow best describes how to use Refixture, while the algorithm section describes the inner workings and methods involved.

# Workflow

![Flowchart](/assets/images/Refixture-Flowchart.png)

## Menu Screen
This is the start and end screen for the program. To close the program, press Esc.

**Create Room**  
: Select this option to start the process of making a room.

**View Room**  
: Select this option to load up a previously saved room to view.

**Feedback**  
: Pressing this activates a link to the website.

**About**  
: Opens a text box detailing the current build.

## View Screen
Displays one of the saved room configurations, allowing you to move around and make changes. When done press the button to return to the menu.

**Navigation**  
: Controls appear in the top right. Use the keyboard keys to move the camera, and hold right click and drag around to change the angle. Scroll wheel and shift adjust the movement speed, allowing finer adjustments.

Control Bar  

: This top panel allows you to control fixtures in the scene. Hovering the mouse on movable objects causes them to glow green. Clicking and dragging will adjust the fixture based on the selected control.

  Move  
  : Click and drag to move the fixture on the horizontal plane.

  Rotate  
  : Click and drag to rotate around the vertical axis.

  Elevate   
  : Click and drag to move the fixture vertically.

  Grid Snap  
  : With this setting enabled, your adjustments will be moved based on uniform increments. This helps aligning different fixtures together, like walls.

**Load / Save**  
: Pressing this opens a window to load and save your room. There will be no room loaded initially. See the documentation on the save window for more [details](#save-window).

## Save Window
Data structures containing the representation of the rooms you create and modify can be saved to the disk in this window.

**File Path**  
: The file path where the saves are located is near the top. Clicking once selects the file path for you to press Ctrl+C to copy, if you want to navigate to the save folder. The files are in a readable .json format.

**Saving, Loading, Deleting**  
: Clicking on the New Save File... entry opens a text box to name your file. Only alphanumeric characters are allowed. After naming, press save. Saving on a previous save overwrites it after a confirmation dialogue. Delete and Load will perform those actions immediately, so don't forget to save first.

**Presets**  
: There may already be some save files in the folder for you to try as examples or presets.

## Setting Screen

**Room Settings**  
: This sub panel displays some important configuration controls.

  __Use Imperial__  
  : This selection will change all numerical values from meters to inches. Your input should also match this setting.

  __Room Width / Length__  
  : Rooms have a set height, but you can modify the room width and length. All fixtures will need to be placed on the foundation unless they are non-moving.

  __Use Genetic Algorithm__  
  : With this setting enabled, the two lists on the side become accessable. They control the list of fixtures you define to be used by the algorithm to arrange within the specified room.

**Static Room Preview**  
: This gives a preview of the room configuration to be used in the next step. What is shown here is static, which means they will not move if using the GA feature to automatically arrange furniture.

  __Edit Room__  
  : This button takes you to the [Edit Room](#edit-room-screen) view where you can modify its contents.

  __Save / Load__  
  : This button opens up the [Save Window](#save-window) for this particular static room configuration. 

**Select Fixtures**  
This panel on the left allows you to define and select fixtures to be used in the genetic algorithm.

  __Dropdown__  
  : This dropdown contains separate categories of furniture selection.

  __Resizing and Selection__  
  : The three input fields next to each fixture control its dimensions and then scales accordingly. Double-clicking the fixture then adds it to the selected list.

**Fixture List**  
This is the list of fixtures to be used by the genetic algorithm to arrange in the room automatically.

  __Removing Fixtures__  
  : Double-click a fixture in the list to remove it.

  __Save / Load__  
  : Save and load list configurations through the [save window](#save-window).

## Edit Room Screen

This screen allows you to edit the static room. Refer to [View Room](#view-room) and Room Settings above as it uses controls from both. In the Edit Room screen, you can add and remove fixtures in real time. Pressing Finshed takes you back to the settings screen where you can save your design.

## Algorithm Screen

Numerous copies of the room are instantiated along a grid. These are each members of the population.

Control Bar
  :Population Count
     The number of rooms in each generation
  :Mutation Rate
     The chance that a single fixture will undergo mutation, causing it to change position each generation.
  :Crossover Rate
     The chance that rooms will form a couple and cross their genetics for the next generation.
  :Generation
     The count of how many generations has passed.
  :Average Fitness
     The average fitness among the population during the last generation.
  :Best Fitness
     The best fitness of a room found among all generations.
  :Reset
     Reseeds the population with new random congfigurations.
  :Start / Pause
     Starts and pauses the evolution process.

## Results Screen

Similar controls as [View Room](#view-room). Displays a selection of the best fit rooms from the evolution in the algorithm screen. Press next to view another room, then make edits and then save the room you like before returning to menu.

# Genetic Algorithm

Genetic algorithms model Darwin's theory of evolution to find solutions to hard problems in computing. Refixture assigns fitness values to rooms based on various factors, and the fittest individuals are able to reproduce and create the next generation of rooms. Reproduction can take values from two rooms and mix them via crossover to produce a new room. Another principle of evolution is mutation, and Refixture applies this randomly each generation to cause furniture to shift.

## Methodology

  Parent Selection
  
  Crossover

  Mutation

  Fitness
