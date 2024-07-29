using System.Linq.Expressions;
using Klueber.Em.Brokers.Brokers.Apis;
using Klueber.Em.Brokers.Brokers.Loggings;
using Klueber.Em.Brokers.Models.ApiModels.Subscription;
using Klueber.Em.Brokers.Models.ApiModels.Tenant;
using Klueber.Em.Brokers.Services.Tenant;
using Moq;
using Tynamix.ObjectFiller;

namespace Klueber.Em.Brokers.Tests.Services
{
    public partial class TenantServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ITenantService tenantService;

        public TenantServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.tenantService = new TenantService(
                apiBrokerMock.Object,
                loggingBrokerMock.Object);
        }
        private static Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
        {
            return actualException => actualException.Message == expectedException.Message
                                      && actualException.InnerException.Message == expectedException.InnerException.Message;
        }
        private List<Tenant> CreateRandomTenantList()
        {
            int randomCount = new IntRange(min: 2, max: 10).GetValue();
            return Enumerable.Range(1, randomCount)
                .Select(item => new Tenant
                {
                    Id = item,
                    Name = new MnemonicString().GetValue(),
                    Subscriptions = Enumerable.Range(1, randomCount)
                        .Select(subItem => new Subscription
                        {
                            Id = subItem,
                            TreeId = 0,
                            Name = new MnemonicString().GetValue(),
                            Klx = string.Empty,
                            Customernumber = string.Empty
                        }).ToList()

                })
                .ToList();

        }
    }
}
