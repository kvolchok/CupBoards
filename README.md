# CupBoards

I focused more on the code and architecture (extensibility, testability, performance) rather than the game itself. As a result, visually, it appears as simple as possible game with basic mechanics.

<table>
    <tr>
        <td>
          Demonstration of the results:
        </td>
        <td>
          PlayMode test example: <br> <i>(Performs 100 random mowes)</i>
        </td>
        <td>
          Unit tests:
        </td>
    </tr>
    <tr>
        <td>
          <img src="https://github.com/kvolchok/CupBoards/blob/master/About/Demo.GIF">
        </td>
        <td>
          <img src="https://github.com/kvolchok/CupBoards/blob/master/About/PlayModeTest.GIF">
        </td>
        <td>
          <img src="https://github.com/kvolchok/CupBoards/blob/master/About/UnitTests.png" width="400">
        </td>
    </tr>
</table>

## Stack

- **Unity 2022.1.21f1**
- **VContainer** (a lightweight, fast DIContainer with code generation capability)
- **UniTask** (a more convenient and modern alternative to coroutines and callbacks)
- **UniTaskPubSub** (an asynchronous MessageBroker to fully separate the Presenter and Model layers)
- **DoTween** (for move animation)

For testing:
- **Unity Test Framework**

## Architecture

For the architecture, I chose a 3-Tier Architecture MVP, which includes a **model layer** (Game settings, Game logic, Service layer, etc.), **view layer** and **presenter layer**.

The model layer knows absolutely nothing about the game's interface layer. All interaction between the model and UI (presenter and view) occurs through an event bus, and if the game lacks an interface, nothing breaks. This makes testing convenient and quick.

For game state management, I went with a state machine as a flexible tool, which clearly outlines responsibilities.

<img src="https://github.com/kvolchok/CupBoards/blob/master/About/Statemachine.png" width="1000"> 
<img src="https://github.com/kvolchok/CupBoards/blob/master/About/UMLCupBoards.png" width="1000">

If needed, you can quickly and easily modify an existing state (adding some audio or visual effects, steps calculator or timer), or add a new state (accrual of points or issuance of bonuses).

You might also find the following classes interesting:

- **GameController** - the first (core) **entry-point** in this project
- **GraphService** - manages creating and comparing graphs
- **PathFinderService** - responsible for finding reachable nodes and routes to them
- **UIController** - the second (UI) **entry-point** in this project
