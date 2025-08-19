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

### Main Menu Options
When you start the game, you'll see:
- **1. New Numerical Tic-Tac-Toe Game**: Start a fresh game (choose Human vs Human or Human vs Computer)
- **L. Load Saved Game**: Browse and resume from available save files with timestamps
- **Q. Quit**: Exit the application

The intuitive interface shows available save files with modification dates, making it easy to resume previous games.

## 🎯 How to Play

### Starting a Game
1. **New Game**: Select "1" from the main menu
   - Choose Human vs Human or Human vs Computer
   - Enter player names and preferences
2. **Load Game**: Select "L" from the main menu
   - Browse available save files with timestamps
   - Select from numbered list or enter filename manually
   - Game resumes exactly where you left off

### Gameplay
3. **Make moves** - Enter moves in format: `row col number`
   - Example: `0 1 5` places number 5 in row 0, column 1
   - Board uses 0-based indexing (0, 1, 2 for each row/column)
4. **Win condition** - Get any line (row, column, diagonal) to sum to 15
5. **Commands available during gameplay**:
   - `help` - Show comprehensive help menu with rules and examples
   - `undo` - Undo the last move (preserves full history)
   - `redo` - Redo an undone move
   - `save <filename>` - Save current game state (auto-adds .json extension)
   - `load <filename>` - Load a saved game mid-session
   - `quit` or `exit` - Exit the current game

### Example Game Session
```
Current Board:
    0   1   2
  +---+---+---+
0 |   |   |   |
  +---+---+---+
1 |   |   |   |
  +---+---+---+
2 |   |   |   |
  +---+---+---+

Alice's turn: 0 0 1
Bob's turn: 0 1 2
Alice's turn: 1 1 5
Bob's turn: 0 2 4
Alice's turn: 2 2 9

🎉 Alice WINS! 🎉
Winning line: Main diagonal (1 + 5 + 9 = 15)

Save this game? (y/n): y
Enter filename: alice-victory
Game saved to alice-victory.json
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
│       ├── NumericalGameRules.cs          # Game rules and win conditions
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
│   └── CommandParser.cs         # Move parsing and command utilities
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
- **`GameSaver`**: Handles complete game state persistence with JSON serialization
- **`HelpSystem`**: Provides contextual help and game rules
- **`CommandParser`**: Parses user input and creates appropriate move objects
- **`GameState`**: Serializable representation of complete game state

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

### Save/Load System
The framework includes a robust save/load system that preserves complete game state:

#### Features
- **Complete State Preservation**: Saves board state, player information, move history, and game-specific data
- **JSON Format**: Human-readable save files with `.json` extension automatically added
- **Cross-Session Compatibility**: Load games from previous sessions seamlessly
- **Undo/Redo History**: Preserves complete move history for continued undo/redo functionality
- **Game Validation**: Ensures save files match the current game type
- **Smart File Management**: Automatic file discovery with timestamp display
- **Player Type Preservation**: Correctly restores Human vs Computer player configurations
- **Seamless Resumption**: No re-initialization prompts when loading games

#### Save File Structure
```json
{
  "gameType": "Numerical Tic-Tac-Toe",
  "saveDate": "2025-08-19T22:30:00.000Z",
  "boardGrid": [[1,0,0],[6,0,0],[0,0,0]],
  "players": [
    {"name": "Dylan", "playerType": "NumericalPlayer", "usesOddNumbers": true}
  ],
  "moveHistory": [
    {"row": 0, "col": 0, "playerName": "Dylan", "number": 1}
  ],
  "gameSpecificData": {"UsedNumbers": [1, 6]}
}
```

#### Usage
**During Gameplay:**
- `save filename` - Saves current game (automatically adds .json extension)
- `load filename` - Loads saved game (tries .json extension if not found)

**From Main Menu:**
- Select "L" or "Load" to browse available save files
- View files with timestamps: `1. alice-victory.json (2025-08-19 23:15)`
- Choose from numbered list or enter filename manually
- Cancel option returns to main menu

**Save File Management:**
- Save files use `.json` extension and are human-readable
- Automatic file discovery in application directory
- Files display last modified date for easy identification
- Cross-session compatibility - resume games from any previous session
- Robust error handling for missing or corrupted files

## 🎯 Features & Enhancements

### ✅ Completed Features
- **Complete Save/Load System**: Full game state preservation with JSON serialization
- **Intuitive User Interface**: Professional main menu with file browsing
- **Robust Error Handling**: Graceful handling of all edge cases
- **Cross-Session Compatibility**: Resume games from any previous session
- **Advanced Move History**: Full undo/redo with state preservation
- **Flexible Player System**: Support for Human vs Human and Human vs Computer
- **Comprehensive Help System**: In-game help with rules and examples
- **Professional Code Quality**: SOLID principles, design patterns, nullable types

### 🚀 Future Enhancements
- **Advanced AI**: Minimax algorithm with difficulty levels
- **Additional Games**: Wild Tic-Tac-Toe, Connect Four, Chess
- **Network Multiplayer**: Online gameplay with matchmaking
- **Graphical Interface**: WPF or web-based UI
- **Game Analytics**: Statistics, player profiles, achievement system
- **Tournament Mode**: Bracket-style competitions

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
- **Code Organisation**: Proper namespace structure and separation of concerns
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

## 🏆 Project Highlights

This Board Game Framework represents a **production-quality implementation** featuring:

- **🎯 Complete Feature Set**: Full save/load, undo/redo, help system, and AI opponent
- **🏗️ Professional Architecture**: Proper design patterns and extensible structure  
- **💾 Advanced Persistence**: Robust JSON-based save system with cross-session compatibility
- **🎮 Polished UX**: Intuitive interface with smart file management and error handling
- **📚 Educational Value**: Demonstrates advanced C# concepts and software engineering principles

**Perfect for**: Learning advanced C# programming, understanding design patterns, or as a foundation for more complex game development projects.

---

**Author**: Dylan Park - Assignment 2 - IFQ563  
**Framework Version**: 1.0 (Production Ready)  
**Last Updated**: August 2025  
**Status**: ✅ Complete with Advanced Features