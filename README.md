# Board Game Framework

A flexible C# console application framework for board games, currently featuring Numerical Tic-Tac-Toe. This project demonstrates object-oriented design patterns and extensible architecture for game development.

## 🎮 Game Overview

### Numerical Tic-Tac-Toe
A unique twist on the classic Tic-Tac-Toe game where players use numbers instead of X's and O's:

- **Player 1**: Uses odd numbers (1, 3, 5, 7, 9)
- **Player 2**: Uses even numbers (2, 4, 6, 8)
- **Objective**: First player to create any line (row, column, or diagonal) that sums to **15** wins!
- **Board**: Standard 3x3 grid
- **Gameplay**: Players take turns placing their numbers on empty positions

### Game Modes
- **Human vs Human**: Two players take turns
- **Human vs Computer**: Play against an AI opponent

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- Windows, macOS, or Linux

### Installation & Running
```bash
# Clone the repository
git clone <repository-url>
cd BoardGameFramework

# Build the project
dotnet build

# Run the game
dotnet run
```

## 🎯 How to Play

1. **Start the game** - Select "1" for Numerical Tic-Tac-Toe from the main menu
2. **Choose game mode** - Human vs Human or Human vs Computer
3. **Make moves** - Enter moves in format: `row col number`
   - Example: `1 2 5` places number 5 in row 1, column 2
4. **Win condition** - Get any line (row, column, diagonal) to sum to 15
5. **Commands available during gameplay**:
   - `help` - Show help menu with rules and examples
   - `undo` - Undo the last move
   - `redo` - Redo an undone move
   - `save <filename>` - Save current game state
   - `load <filename>` - Load a saved game
   - `quit` or `exit` - Exit the game

### Example Game
```
Current Board:
  1   2   3
1 .   .   .
2 .   .   .
3 .   .   .

Player 1 (Odd numbers): Enter move (row col number): 1 1 1
Player 2 (Even numbers): Enter move (row col number): 1 2 2
Player 1 (Odd numbers): Enter move (row col number): 2 2 5
Player 2 (Even numbers): Enter move (row col number): 1 3 4
Player 1 (Odd numbers): Enter move (row col number): 3 3 9

Player 1 wins! (Diagonal: 1 + 5 + 9 = 15)
```

## 🏗️ Architecture & Design Patterns

### Core Design Patterns

1. **Template Method Pattern**
   - `Game` class defines the game flow algorithm
   - Subclasses implement game-specific behavior

2. **Factory Method Pattern**
   - `GameFactory` creates game-specific objects
   - `NumericalTicTacToeFactory` creates Numerical Tic-Tac-Toe components

3. **Strategy Pattern**
   - Different player types (Human, Computer) with different move strategies

### Project Structure
```
BoardGameFramework/
├── Core/                          # Core game framework
│   ├── Game.cs                   # Abstract game template
│   ├── Board.cs                  # Game board implementation
│   ├── Move.cs                   # Abstract move class
│   └── GameFactory.cs            # Abstract factory for games
├── Games/                        # Game implementations
│   └── NumericalTicTacToe/
│       ├── NumericalTicTacToeGame.cs      # Game logic
│       ├── NumericalTicTacToeFactory.cs   # Factory implementation
│       ├── NumericalMove.cs               # Move implementation
│       ├── NumericalGameRules.cs          # Game rules
│       └── NumericalTicTacToeHelpSystem.cs # Help system
├── Players/                      # Player implementations
│   ├── Player.cs                 # Abstract player base
│   ├── HumanPlayer.cs           # Human player implementation
│   ├── ComputerPlayer.cs        # Computer player base
│   ├── NumericalPlayer.cs       # Numerical game human player
│   └── NumericalComputerPlayer.cs # Numerical game AI player
├── Services/                     # Game services
│   ├── GameSaver.cs             # Save/load functionality
│   ├── MoveHistory.cs           # Undo/redo system
│   ├── HelpSystem.cs            # Abstract help system
│   └── CommandParser.cs         # Command parsing utilities
├── UI/                          # User interface
│   └── GameConsole.cs           # Console-based UI
├── Interfaces/                  # Shared interfaces
│   └── INumberedPlayer.cs       # Interface for numbered players
└── Program.cs                   # Application entry point
```

### Key Classes

#### Core Framework
- **`Game`**: Abstract base class implementing the Template Method pattern for game flow
- **`Board`**: Manages the game grid and move validation
- **`Move`**: Abstract representation of a player move
- **`GameFactory`**: Factory Method pattern for creating game instances

#### Game Implementation
- **`NumericalTicTacToeGame`**: Concrete implementation with number-based rules
- **`NumericalMove`**: Move class that includes the number being placed
- **`NumericalGameRules`**: Encapsulates win condition logic (sum to 15)

#### Player System
- **`Player`**: Abstract base for all player types
- **`HumanPlayer`**: Handles human input and move parsing
- **`ComputerPlayer`**: Base for AI players with thinking delay
- **`NumericalPlayer`**: Human player with odd/even number constraints

#### Services
- **`MoveHistory`**: Implements undo/redo functionality using stacks
- **`GameSaver`**: Handles game state persistence (basic implementation)
- **`HelpSystem`**: Provides contextual help and game rules

## 🔧 Technical Details

### Technology Stack
- **.NET 8.0** with C# nullable reference types
- **Console Application** with rich text-based UI
- **MSBuild** for compilation and dependency management
- **Pure .NET BCL** - no external dependencies

### Build Configuration
- Target Framework: `net8.0`
- Nullable Reference Types: Enabled
- Implicit Usings: Enabled
- Output Type: Console Executable

### Code Quality Features
- Comprehensive error handling
- Nullable reference type safety
- XML documentation comments
- Consistent naming conventions
- SOLID principles adherence

## 🎯 Current Limitations & Future Enhancements

### Known Issues
- **Save/Load System**: Currently only saves basic metadata, not full game state
- **AI Intelligence**: Computer player uses basic random strategy
- **File Extensions**: Save files don't automatically get extensions

### Planned Enhancements
- Complete save/load implementation with JSON serialization
- Advanced AI strategies (minimax algorithm)
- Additional game variants (Wild Tic-Tac-Toe)
- Network multiplayer support
- Graphical user interface
- Game statistics and player profiles

## 🧪 Testing & Development

### Building the Project
```bash
# Clean build
dotnet clean

# Build with detailed output
dotnet build --verbosity normal

# Build for release
dotnet build --configuration Release
```

### Development Commands
```bash
# Restore dependencies
dotnet restore

# Run without building
dotnet run --no-build

# Build and run
dotnet run
```

## 📚 Educational Context

This project was developed as **Assignment 2 for IFQ563** and demonstrates:

- **Object-Oriented Design**: Inheritance, polymorphism, encapsulation
- **Design Patterns**: Template Method, Factory Method, Strategy
- **Code Organization**: Proper namespace structure and separation of concerns
- **Error Handling**: Robust exception handling and user feedback
- **Extensibility**: Framework designed for easy addition of new games

## 🤝 Contributing

The framework is designed for extensibility. To add a new game:

1. Create a new folder under `Games/`
2. Implement `Game` abstract class
3. Create corresponding `GameFactory` subclass
4. Implement game-specific `Move` and `Player` classes
5. Register the factory in `GameConsole`

## 📄 License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

---

**Author**: Dylan Park - Assignment 2 (IFQ563)  
**Framework Version**: 1.0  
**Last Updated**: August 2025