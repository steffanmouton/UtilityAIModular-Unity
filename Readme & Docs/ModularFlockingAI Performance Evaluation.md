# ModularFlockingAI Performance Evaluation

### Issues Encountered
I found it difficult to program in such a way that the system is truly modular, yet robust. I was able to get something that is truly modular, but had to make sacrifices on the side of depth. 

### Performance
This system works well, given you work within the expected parameters. I've implemented behaviours for Seek, Flee, Wander, and Arrive besides just the flocking mechanic. These classic steering behaviours all work independently and with each other, but in combination with flocking things get messy. It would require a lot of trial and error, tweaking many different values, to get flocking that ALSO cleanly implements these behaviours. They *work* in that the forces are all applied appropriately. But getting natural-looking behaviour will take some effort.