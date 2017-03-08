
using Xunit;

namespace KptBoardService.Tests
{
    public class CommandTests
    {
        [Fact(DisplayName = "CommandはURLを解析できる")]
        public void TestGetCommand()
        {
            Assert.True(Command.GetCommand("/v1/users").IsUsers);
            Assert.True(Command.GetCommand("/v1/users/").IsUsers);
            Assert.True(Command.GetCommand("/v1/users/4").IsUsers);

            Assert.Equal(4, Command.GetCommand("/v1/users/4").UserId);
            Assert.Equal(4, Command.GetCommand("/v1/users/4/kptboards").UserId);

            Assert.True(Command.GetCommand("/v1/users/4/kptboards").IsKptBoards);
            Assert.True(Command.GetCommand("/v1/users/4/kptboards/5").IsKptBoards);

            Assert.Equal(5, Command.GetCommand("/v1/users/4/kptboards/5").KptBoardId);
        }
    }
}