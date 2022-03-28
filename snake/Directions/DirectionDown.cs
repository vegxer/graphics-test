using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake.Directions
{
    public class DirectionDown : Direction
    {
        public override Direction Turn180()
        {
            return new DirectionUp();
        }

        public override Direction TurnClockwise()
        {
            return new DirectionLeft();
        }

        public override Direction TurnCounterClockwise()
        {
            return new DirectionRight();
        }
    }
}
