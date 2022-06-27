using System;
using ContactsApp.Core.Extensions;
using Xunit;

namespace ContactsApp.Core.Test.Extensions
{

    public class DateTimeExtensionsTests
    {
        [Fact]
        public void SetKindUtcNullInputTest()
        {
            DateTime? input = null;
            DateTime? result = input.SetKindUtc();
            Assert.Null(result);
        }

        [Fact]
        public void SetKindUtcNonNullRegularDateInputTest()
        {
            DateTime? input = DateTime.Now;
            DateTime? result = input.SetKindUtc();
            Assert.NotNull(result);
            /* below is the primary functionality.  if the input did not have a "Kind" set, it gets set to DateTimeKind.Utc */
            Assert.Equal(DateTimeKind.Utc, result.Value.Kind);
        }

        [Fact]
        public void SetKindUtcNonNullOffsetDateInputTest()
        {
            DateTime? input = DateTime.Now;
            DateTime withKindUtcInput = DateTime.SpecifyKind(input.Value, DateTimeKind.Utc);
            DateTime? result = withKindUtcInput.SetKindUtc();
            Assert.NotNull(result);
            /* Utc "in" remains "Utc" out */
            Assert.Equal(DateTimeKind.Utc, result.Value.Kind);
        }
    
        [Fact]
        public void UnspecifiedKindIsOverwrittenTest()
        {
            DateTime? input = DateTime.Now;
            DateTime withKindUtcInput = DateTime.SpecifyKind(input.Value, DateTimeKind.Unspecified);
            DateTime? result = withKindUtcInput.SetKindUtc();
            Assert.NotNull(result);
            /* note the behavior.  "DateTimeKind.Unspecified" with overwritten with DateTimeKind.Utc */
            Assert.Equal(DateTimeKind.Utc, result.Value.Kind);
        }
    
        [Fact]
        public void LocalKindIsOverwrittenTest()
        {
            DateTime? input = DateTime.Now;
            DateTime withKindUtcInput = DateTime.SpecifyKind(input.Value, DateTimeKind.Local);
            DateTime? result = withKindUtcInput.SetKindUtc();
            Assert.NotNull(result);
            /* note the behavior.  "DateTimeKind.Local" with overwritten with DateTimeKind.Utc */
            Assert.Equal(DateTimeKind.Utc, result.Value.Kind);
        }    
        
    }

}