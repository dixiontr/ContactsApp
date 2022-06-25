using System;
using ContactsApp.Core.Interfaces.DTO;
using ContactsApp.Core.Interfaces.Entity;
using ContactsApp.Core.Mappers;
using Moq;
using Xunit;

namespace ContactsApp.Core.Test.Mappers
{

    public class AutoMapper_Test
    {
        private record _testEntity : IEntity
        {
            public Guid Id { get; set; }
            public DateTime CreatedOn { get; set; }
            public DateTime UpdatedOn { get; set; }
            public string Name { get; set; }
            public bool IsActive { get; set; }
            public int Age { get; set; }
        }

        private record _testDto : IDto
        {
            public string Name { get; set; }
            public bool IsActive { get; set; }
            public int Age { get; set; }
        }

        [Fact]
        public async void Test_Entity_to_DTO()
        {
            _testEntity testEntity = new _testEntity()
            {
                Age = 15,
                Name = "Jane",
                IsActive = true
            };
            _testDto testDto = new _testDto()
            {
                Age = 15,
                Name = "Jane",
                IsActive = true
            };

            var mapperResult = testEntity.AsDto<_testDto>(new _testDto());
            
            Assert.Equal(testDto,mapperResult);
        }
        [Fact]
        public async void Test_DTO_to_Entity()
        {
            _testEntity testEntity = new _testEntity()
            {
                Age = 15,
                Name = "Jane",
                IsActive = true
            };
            _testDto testDto = new _testDto()
            {
                Age = 15,
                Name = "Jane",
                IsActive = true
            };

            var mapperResult = testDto.AsEntity<_testEntity>(new _testEntity());
            
            Assert.Equal(testEntity,mapperResult);
        }

    }

}