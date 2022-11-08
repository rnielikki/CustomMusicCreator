using CustomMusicCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMusicCreatorTest
{
    public class SgdConverterTest
    {
        [Fact]
        public void SgdConvertTest()
        {
            using var source = File.OpenRead($"testfiles\\aa3s\\RightFormat.aa3");
            using var result = new SgdConverter().ConvertOne(source, "test");

            //should throw exception, when sgd conversion is failed.
            using var source2 = File.OpenRead($"testfiles\\aa3s\\WrongFormat.aa3");
            Assert.Throws<ArgumentException>(() =>
            {
                using (new SgdConverter().ConvertOne(source2, "test")) { }
            });
        }
    }
}
