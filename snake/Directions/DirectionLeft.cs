using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake.Directions
{
    public class DirectionLeft : Direction
    {
        public override Direction Turn180()
        {
            return new DirectionRight();
        }

        public override Direction TurnClockwise()
        {
            return new DirectionUp();
        }

        public override Direction TurnCounterClockwise()
        {
            return new DirectionDown();
        }
    }
}
