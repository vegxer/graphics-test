using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake.Directions
{
    public abstract class Direction
    {
        public abstract Direction TurnClockwise();
        public abstract Direction TurnCounterClockwise();
        public abstract Direction Turn180();
    }
}
