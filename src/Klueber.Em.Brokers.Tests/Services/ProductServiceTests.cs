using Klueber.Em.Brokers.Brokers.Apis;
using Klueber.Em.Brokers.Brokers.Loggings;
using Klueber.Em.Brokers.Services.Product;
using Moq;
using Tynamix.ObjectFiller;

namespace Klueber.Em.Brokers.Tests.Services;

public partial class ProductServiceTests
{
    private readonly Mock<IApiBroker> apiBrokerMock;
    private readonly Mock<ILoggingBroker> loggingBrokerMock;
    private readonly IProductService productService;

    public ProductServiceTests()
    {
        this.apiBrokerMock = new Mock<IApiBroker>();
        this.loggingBrokerMock = new Mock<ILoggingBroker>();
        
        this.productService = new ProductService(
            loggingBrokerMock.Object,
            apiBrokerMock.Object);
    }

    private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();
    
    private List<dynamic> CreateRandomKlueberProducts()
    {
        int randomCount = GetRandomNumber();
        return Enumerable.Range(1, randomCount).Select(item => new
        {
            Id = item,
            Number = item.ToString(),
            Term = new MnemonicString().GetValue()
        }).ToList<dynamic>();
    }
}