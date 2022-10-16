using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;


namespace linqSnippet
{


    public class Snippets
    {
        static public void BasicLinq()
        {
            string[] cars =
            {
                "VW Golf",
                "VW AMAROK",
                "Audi A3",
                "Fiat mobi",
                "Toyota Corolla",
                "Toyota Hilux",
                "NISSAN  Frontier"

            };


            //1 SELECT *  of cars (ALL)

            var carList = from car in cars select car;
            foreach (var car in carList)
            {
                Console.WriteLine(car); 
            }

            // 2. SELECT WHERE   car is VW (SELECT VW)
            var vwList = from car in cars where car.Contains("VW") select car;
            foreach (var car in vwList)
            {
                Console.WriteLine(car);
            }


           

            

        }
        //Number examples
        static public  void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //1. Each Number multiplied by 3

            //take all numbers, but  9

            //Order numbers bue ascending value

            var processedNumberList = numbers
                                        .Select(num => num * 3)// *3 
                                        .Where(num => num != 9)// !=9
                                        .OrderBy(num => num);//order ascending
        
        }
        static public void SearchExamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c"
            };
            //1. first of all elements

            var first = textList.First();

            //2. first element that has "c"

            var cText = textList.First(text => text.Equals("c"));

            // 3.first element that constains "j"
            var jText = textList.First(text => text.Contains("j"));

            //4. first element that contains "z" or default

            var firstOrDefaultText = textList.FirstOrDefault(text => text.Contains("z"));//" " o primer un valor con "z"


            //5. last element that contains "z"  or default
            var lasttOrDefaultText = textList.LastOrDefault(text => text.Contains("z"));//" " o  ultimo un valor con "z"

            //6 .  single Values

            var uniqueText = textList.Single();
            var uniqueOrDefaultText = textList.SingleOrDefault();



            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEventNumbers = { 0, 2, 6 };

            // 7 obtain {4,8}

            var myEveneNumbers= evenNumbers.Except(otherEventNumbers); // {4,8}


        }

        static public void MultipleSelects()
        {
            //SELECT MANY
            string[] myOpinions =
            {
                "opinion 1 , text1",
                "opinion 2, text2",
                "opinion 3, text3",
                "opinion 4, text4",
                "opinion 5, text5",
            };

            var myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id=1,
                    Name="Enter 1",
                    Employees= new[]
                    {
                        new Employee
                        {
                            Id=1,
                            Name="Bauti",
                            Email="bauti@test.com",
                            Salary=1000
                        },
                        new Employee
                        {
                            Id=2,
                            Name="Nico",
                            Email="nicoi@test.com",
                            Salary=3000
                        },
                        new Employee
                        {
                            Id=3,
                            Name="Picky",
                            Email="picky@test.com",
                            Salary=2000
                        }
                    }

                },

                 new Enterprise()
                {
                    Id=2,
                    Name="Enter 2",
                    Employees= new[]
                    {
                        new Employee
                        {
                            Id=4,
                            Name="obi",
                            Email="obi@test.com",
                            Salary=4000
                        },
                        new Employee
                        {
                            Id=5,
                            Name="mando",
                            Email="mandoi@test.com",
                            Salary=5000
                        },
                        new Employee
                        {
                            Id=6,
                            Name="yoda",
                            Email="yoda@test.com",
                            Salary=6000
                        }
                    }

                }
            };

            //obtain all employees of all enterprises
            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            //know if any list is empty

            bool hasEnterprises = enterprises.Any();

            bool hasEmployees = enterprises.Any(x => x.Employees.Any());


            //all enterprises at least has an employee with more than 1000 of salary

            bool hasEmployeeWithSalaryMoreThan1000 =
                enterprises.Any(enterprise => enterprise.Employees.Any(employe => employe.Salary > 1000));


        }
      
        static public void lingCollections()
        {
            var firstList = new List<string>() { "a","b","c"};
            var secondList = new List<string>() { "a", "c", "d" };


            //inner join

            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };


            var commonResult2 = firstList.Join(
                                secondList,
                                element => element,
                                secondElement => secondElement,
                                (element, secondElement) => new { element, secondElement });


            //outer join -left
            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };

            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = secondElement };
            //outer join -right

            var rightOuterJoin = from secondElement in secondList
                                join element in firstList
                                on secondElement equals element
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where secondElement != temporalElement
                                select new { Element = secondElement };

            //union

            var unionList = leftOuterJoin.Union(rightOuterJoin);

        }

        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };

            var skipTwoFirstValues = myList.Skip(2);//{3,4,5,6,7,8,9,10}

            var skipLasttwoValues = myList.SkipLast(2);//{1,2,3,4,5,6,7,8}

            var skipWhileSmallerThan4 = myList.SkipWhile(num => num < 4);//{5,6,7,8,9,10}

            //take
            var takeFristtwoValues = myList.Take(2);//{1,2}
            var takeLasttwoValues = myList.TakeLast(2);//{9,10}
            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4);//{1,2,3,4}

          
        }

        //paging  skip and take
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultPerPage)
        {
            int stratIndex = (pageNumber - 1) * resultPerPage;
            return collection.Skip(stratIndex).Take(resultPerPage);
        }

        // Variables

        static public void LinqVariables()
        {
            int[] numbers = {1, 2, 3, 4, 5, 6,7,8,9, 10};

            var aboveAverage = from number in numbers
                               let averageNum = numbers.Average()
                               let nSquared = Math.Pow(number, 2)
                               where nSquared > averageNum
                               select number;

            Console.WriteLine("Average:  {0}", numbers.Average());
            foreach (int number in aboveAverage)
            {
                Console.WriteLine("Number : {0} ,Sqaure: {1}",number, Math.Pow(number, 2));
            }
        }

        //zip
        static public void LinqZip()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "uno", "dos", "tres", "cuatro", "cinco" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers,(number,word)=>number+ "="  +word);
        }

        //repeat  & range
        static public void repeatRangeLing()
        {
            //Generate  collection from 1 -1000----<Range
            IEnumerable<int> first1000 = Enumerable.Range(1, 1000);

            // Repear a value n times
            IEnumerable<string> fiveX = Enumerable.Repeat("X", 5);         
            
        }

        static public void studentLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name="Bauti",
                    Grade=90,
                    Certified=true,
                },
                new Student
                {
                    Id = 2,
                    Name="Picky",
                    Grade=50,
                    Certified=false,
                },
                new Student
                {
                    Id = 3,
                    Name="Nico",
                    Grade=80,
                    Certified=true,
                },
                new Student
                {
                    Id = 4,
                    Name="Pedro",
                    Grade=35,
                    Certified=true,
                },
                new Student
                {
                    Id = 5,
                    Name="Orco",
                    Grade=25,
                    Certified=false,
                }
            };

            var certifiedStudents = from student in classRoom
                                    where student.Certified == true
                                    select student;

            var noCertifiedStudent = from student in classRoom
                                     where  student.Certified==false
                                     select student;

            var approvedStudent = from student in certifiedStudents
                                  where student.Grade >= 50
                                  select student.Name;
        }
        //all
        static public void ALLLInq()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };

            bool allAreSmallerThan10 = numbers.All(x=>x<10); //true

            bool allAreBiggerOrEqualsThan2 = numbers.All(x=>x>=2);//false

            var emptyList = new List<int>();

            bool allNumbersAreGreaterThan0= numbers.All(x=>x >=0);//TRUE  (busca el valor q no cumple!!!!!!!


        }

        //Aggregate

        static public  void aggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //sum all numbers
            int sum = numbers.Aggregate((prevSum,current)=>prevSum + current);

            string[] words = { "Hello,", "my", "name", "is", "Bauti" };

            string greating = words.Aggregate((prevGreeting, current) => prevGreeting + current);
        
        
        }

        //disctint
        static public  void distinctValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
            IEnumerable<int> distinctValues = numbers.Distinct();

        }
        //GroupBy
        static public void groupByExamples()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //Obtain only even numbers and generate two groups

            var grouped = numbers.GroupBy(x => x % 2 == 0);


            //We will have two group
            //1 group that desnt fit  the condition (odd numbers)
            //2 group that fits the condition (even numbers)

            foreach (var item in grouped)
            {
                foreach (var value in item)
                {
                    Console.WriteLine(value);//1,3,5,7,9........,2,4,6,8(1er odds , then evene)
                }
            }


            //another example

            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name="Bauti",
                    Grade=90,
                    Certified=true,
                },
                new Student
                {
                    Id = 2,
                    Name="Picky",
                    Grade=50,
                    Certified=false,
                },
                new Student
                {
                    Id = 3,
                    Name="Nico",
                    Grade=80,
                    Certified=true,
                },
                new Student
                {
                    Id = 4,
                    Name="Pedro",
                    Grade=35,
                    Certified=true,
                },
                new Student
                {
                    Id = 5,
                    Name="Orco",
                    Grade=25,
                    Certified=false,
                }
            };

            var certifiedQuery = classRoom.GroupBy(x => x.Certified && x.Grade >= 50);

            //we have 2 groups
            //1ero  not certifed
            //2do   certified

            foreach (var group in certifiedQuery)
            {

                Console.WriteLine("-----------{0}----------", group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine(student.Name);
                }
            }
        } 

        static public void relationsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id=1,
                    Title="My first post",
                    Content="My first content",
                    Created= DateTime.Now,
                    Comments= new List<Comment>()
                    {
                        new Comment()
                        {
                            Id=1,
                            Created=DateTime.Now,
                            Title="My first comment",
                            Content="My first content comment"
                        },
                        new Comment()
                        {
                            Id=2,
                            Created=DateTime.Now,
                            Title="My Second comment",
                            Content="My Second content comment"
                        }
                    }

                },
                 new Post()
                {
                    Id=2,
                    Title="My Second post",
                    Content="My Secondt content",
                    Created= DateTime.Now,
                    Comments= new List<Comment>()
                    {
                        new Comment()
                        {
                            Id=3,
                            Created=DateTime.Now,
                            Title="My first comment del segundo",
                            Content="My first content comment del segundo"
                        },
                        new Comment()
                        {
                            Id=4,
                            Created=DateTime.Now,
                            Title="My Second comment del segundo",
                            Content="My Second content comment del segundo"
                        }
                    }

                }
            };

            var commentsContent = posts.SelectMany(
                post => post.Comments,
                (post, comment) => new { PostId = post.Id, CommentContent = comment.Content });

        }
    }

}