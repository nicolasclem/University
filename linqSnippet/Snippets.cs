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
                enterprises.Any(enterprise => enterprise.Employees.Any(employe => employe.Salary > 1000))


        };
      
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
                                 from secondElement in secondList.Where (s => s== element).DefaultIfEmpty()
                                 select new {Element = element ,SecondElement = secondElement}
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
    }

}