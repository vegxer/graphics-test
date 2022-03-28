using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake.Directions
{
    public class DirectionRight : Direction
    {
        public override Direction Turn180()
        {
            return new DirectionLeft();
        }

        public override Direction TurnClockwise()
        {
            return new DirectionDown();
        }

        public override Direction TurnCounterClockwise()
        {
            return new DirectionUp();
        }
    }
}
