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
This is the start and end screen for the program.

### Create Room
Select this option to start the process of making a room.

### View Room
Select this option to load up a previously saved room to view.

### Feedback
Pressing this activates a link to the website.

### About
Opens a text box detailing the current build.

## View Screen
Displays one of the saved room configurations, allowing you to move around and make changes. When done press the button to return to the menu.

### Navigation
Controls appear in the top right. Use the keyboard keys to move the camera, and hold right click and drag around to change the angle. Scroll wheel and shift adjust the movement speed, allowing finer adjustments.

### Control Bar
This top panel allows you to control fixtures in the scene. Hovering the mouse on movable objects causes them to glow green. Clicking and dragging will adjust the fixture based on the selected control.

#### Move
Click and drag to move the fixture on the horizontal plane.

#### Rotate
Click and drag to rotate around the vertical axis.

#### Elevate
Click and drag to move the fixture vertically.

#### Grid Snap
With this setting enabled, your adjustments will be moved based on uniform increments. This helps aligning different fixtures together, like walls.

### Load / Save
Pressing this opens a window to load and save your room. There will be no room loaded initially. See the documentation on the save window for more [details](## Save Window).

## Save Window
Data structures containing the representation of the rooms you create and modify can be saved to the disk in this window.

### File Path
The file path where the saves are located is near the top. Clicking once selects the file path for you to press Ctrl+C to copy, if you want to navigate to the save folder. The files are in a readable .json format.

### Saving, Loading, Deleting
Clicking on the New Save File... entry opens a text box to name your file. Only alphanumeric characters are allowed. After naming, press save. Saving on a previous save overwrites it after a confirmation dialogue. Delete and Load will perform those actions immediately, so don't forget to save first.

### Presets
There may already be some save files in the folder for you to try as examples or presets.

## Setting Screen

### Room Settings

#### Use Imperial

#### Room Width / Length

#### Use Genetic Algorithm

### Static Room Preview

#### Edit Room

#### Save / Load

### Select Fixtures

#### Dropdown

#### Resizing and Selection

### Fixture List

#### Removing Fixtures

#### Save / Load

## Edit Room Screen



## Algorithm Screen



## Results Screen

# Genetic Algorithm

## Methodology