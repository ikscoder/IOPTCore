using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace IOPTCore.Models
{
    public static class TestData
    {
        public static void GenerateTestData(IServiceProvider serviceProvider)
        {

            var scripts = new HashSet<Script>()
            {
                new Script(-1,"Name","Name","Some code",-1),
                new Script(-2,"Name1","Name1","Some other code",-1)
            };
            var props = new HashSet<Property>()
            {
                new Property(-1,"LED","LED",(int)TypeCode.Boolean,-1,scripts,"false"),
                new Property(-2,"XPosition","XPosition",(int)TypeCode.Int32,-1,null,"0"),
                new Property(-3,"ButtonState","ButtonState",(int)TypeCode.Boolean,-1,null,"false")
            };

            var obj = new HashSet<Object>() { new Object(-1, "TestObject", "TestObject", -1, props) };

            var model = new Model(-1, "TestModel", "TestModel", obj);
            Snapshot.current.models.Add(model);

            var context = serviceProvider.GetService<DataContext>();
            foreach (var a in Snapshot.current.models)
            {
                context.Models.Add(a);
                foreach (var b in a.objects)
                {
                    b.modelId = a.id;
                    context.Objects.Add(b);
                    foreach (var c in b.properties)
                    {
                        c.objectId = b.id;
                        context.Properties.Add(c);
                        foreach (var d in c.scripts)
                        {
                            d.propertyId = c.id;
                            context.Scripts.Add(d);
                        }
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
