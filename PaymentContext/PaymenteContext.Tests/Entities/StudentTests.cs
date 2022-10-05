using PaymenteContext.Domain.Entity;

namespace PaymenteContext.Tests;

[TestClass]
public class StudentTests
{
    [TestMethod]
    public void TestMethod1()
    {
        var subscription = new Subscription(null);
        var student = new Student("Lucas", "Mota", "exe@email.com", "1232456", "rua das pedrinhas");

        student.AddSubscription(subscription);

    }
}