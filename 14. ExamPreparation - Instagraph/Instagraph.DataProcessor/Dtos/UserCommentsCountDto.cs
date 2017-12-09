using System;
using System.Collections.Generic;
using System.Text;

namespace Instagraph.DataProcessor.Dtos
{
    public class UserCommentsCountDto
    {
        public string Username { get; set; }

        public int MaxCommentsCount { get; set; }
    }
}
