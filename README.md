# Unity Solitaire Undo System Prototype

## Overview

This is a Unity prototype implementing an "Undo Move" system for a simplified Solitaire card game, developed as a technical assessment for a Senior Unity Developer position. The project was completed within a 2-hour time constraint using AI-assisted development tools.

## Controls

- **Mouse Drag**: Move cards between piles
- **U Key**: Undo last move
- **Right Click**: Alternative undo trigger

## What Was Built

### Core Features

- **Solitaire Game Structure**: Basic solitaire layout with foundations, tableau, deck, and waste pile
- **Drag & Drop Interface**: Card movement using Unity's UI system with mouse drag controls
- **Undo System**: Command pattern implementation supporting move reversal (U key or right-click)
- **Card Movement Validation**: Basic rules for valid card placement between piles

### Technical Implementation

- **Command Pattern**: Robust undo system using Command pattern
- **Modular Architecture**: Clear separation between model (game logic) and view (UI presentation)
- **Type Safety**: Explicit nullable reference types throughout the codebase
- **Clean Code Practices**: Well-structured classes with single responsibilities

### Current Limitations

- **Incomplete Game Logic**: Deck and waste pile functionality is not fully implemented
- **No Win Condition**: Game doesn't detect or handle completion states
- **Single Screen**: No navigation system or multiple game states
- **Basic Visuals**: Minimal UI without animations or polished graphics

## Future Improvements

Given more development time, the following enhancements would be prioritized:

### Architecture

- **Navigation System**: Screen management with open/close animations for multi-screen applications
- **MVVM/MVP Pattern**: Better and more consistent separation between architectural levels
- **MVVM+ECS**: Combination of MVVM for app level logic and ECS for gameplay logic due to its modularity, which indirectly provides AI-development ease, if the team is familiar and comfortable with ECS paradigm

### User Experience

- **Animation System**: Implement tween animations (e.g., PrimeTween) for smooth card movements
- **Visual Polish**: Source professional graphics from design team, Asset Store, or AI generation
- **Complete Game Logic**: Full solitaire rules implementation with win conditions

### Code Quality

- **Comprehensive Testing**: Unit tests for game logic and optional integration tests for UI
- **Performance Optimization**: Object pooling for cards and optimized rendering

## Development Approach

### AI-Assisted Workflow

The project was developed using **Aider CLI** with **Sonnet 4/Haiku 3.5** model pair and **Rider IDE**. The AI assistance strategy focused on:

- **Modular Prompting**: Requesting isolated, single-responsibility classes
  - Example: _"Write SolitaireInput class that controls card drag and drop input for a solitaire game using Unity Canvas"_
- **Technical Task Focus**: Preferring specific technical improvements over broad product requirements
  - Example: _"GameView and SolitaireBoards share responsibilities inadequately, improve SolitaireBoardView encapsulation"_
- **Manual Review Process**: Disabled automatic commits to allow thorough code review and manual refinement

This approach maintained better code quality by avoiding large, cluttered contexts that typically lead to suboptimal AI-generated code.

---

This README was written outside the time limit and with AI assistance
