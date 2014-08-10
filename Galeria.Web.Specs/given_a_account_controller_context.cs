using Machine.Specifications;
using Galeria.Domain.Services;
using Galeria.Web.Controllers;
using Moq;

namespace Galeria.Web.Specs
{
    public class given_a_account_controller_context
    {
        private static Mock<IReadOnlyRepository> _mockReadOnlyRepository;
        private static Mock<IWriteOnlyRepository> _mockWriteOnlyRepository;
        private static AccountController _accountController;

        private Establish context = () =>
            {
                _mockReadOnlyRepository = new Mock<IReadOnlyRepository>();
                _mockWriteOnlyRepository = new Mock<IWriteOnlyRepository>();
                _accountController = new AccountController(_mockReadOnlyRepository.Object,
                                                           _mockWriteOnlyRepository.Object);
            };
    }
}