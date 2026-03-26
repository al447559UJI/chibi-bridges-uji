# Chibi Bridges

## ℹ️ About
Videogame demo made in Unity 6000.3.0f1 as an assignment for my Project I class at Jaume I University.

### 📋 List of features

**Movement:**
Sophisticated 2D movement and jump mechanics with parametrized variables, examples include:
- Acceleration/deceleration
- Jump hang, buffer, variable time.
- Fast fall, coyote time.

**Parallel state machines:**
The protagonist counts with two decoupled state machines that communicate with each other cleanly.

**Enemy AI:**
The Tank enemy features an Enum-based state machine with coroutines for patrol and shoot behaviors. On it's movement we can also find platform edge and wall detection logic.
