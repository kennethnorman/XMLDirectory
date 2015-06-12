using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XMLDirectoryTest
{
    class CommonTest
    {
        public static void CompareResult(StreamReader ExpectedResult, StreamReader ActualResult)
        {
            while (true)
            {
                if (ExpectedResult.Read() != ActualResult.Read())
                {
                    Assert.Fail();
                    break;
                }
                if (ExpectedResult.EndOfStream && ActualResult.EndOfStream)
                {
                    break;
                }
                if (ExpectedResult.EndOfStream || ActualResult.EndOfStream)
                {
                    Assert.Fail();
                    break;
                }
            }
        }
    }
}
