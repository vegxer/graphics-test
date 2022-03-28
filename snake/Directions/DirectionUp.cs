using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake.Directions
{
    public class DirectionUp : Direction
    {
        public override Direction Turn180()
        {
            return new DirectionDown();
        }

        public override Direction TurnClockwise()
        {
            return new DirectionRight();
        }

        public override Direction TurnCounterClockwise()
        {
            return new DirectionLeft();
        }
    }
}
