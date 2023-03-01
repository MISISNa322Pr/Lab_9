using System;
using System.Text;
using System.IO;

namespace _7_Lab {

    class Task_3_1 {
        public string Name;
        public double AverageMark;

        public Task_3_1(string name, int[] marks) {
            this.Name = name;
            this.AverageMark = marks.Sum() / 5.0;
        }
    }

    class Task_3_4 {
        public string Surname;
        public double Result;

        public Task_3_4(string surname, double result) {
            this.Surname = surname;
            this.Result = result;
        }
    }

    class Task_3_6 {

        public Dictionary<string, int> first = new Dictionary<string, int>();
        public Dictionary<string, int> second = new Dictionary<string, int>();
        public Dictionary<string, int> third = new Dictionary<string, int>();

        public void addAnswer(string[] answer) {
            if (answer[0] == "-") {

            } else if (first.ContainsKey(answer[0])) {
                first[answer[0]]++;
            } else {
                first.Add(answer[0], 1);
            }

            if (answer[1] == "-") {

            } else if (second.ContainsKey(answer[1])) {
                second[answer[1]]++;
            } else {
                second.Add(answer[1], 1);
            }

            if (answer[2] == "-") {

            } else if (third.ContainsKey(answer[2])) {
                third[answer[2]]++;
            } else {
                third.Add(answer[2], 1);
            }
        }


        public void printPopularAnswers() {

            void printAnswer(Dictionary<string, int> dict, string name) {

                string[] keys = new string[dict.Count];
                int[] values = new int[dict.Count];

                int curInd = 0;
                foreach (var item in dict.Keys) {
                    keys[curInd] = item;
                    values[curInd] = dict[item];
                    curInd++;
                }

                for (int i = 0; i < keys.Length; i++) {
                    for (int j = i + 1; j < keys.Length; j++) {
                        if (values[i] < values[j]) {
                            int tempi = values[i];
                            values[i] = values[j];
                            values[j] = tempi;

                            string temps = keys[i];
                            keys[i] = keys[j];
                            keys[j] = temps;
                        }
                    }
                }

                int suma = values.Sum();


                string output_path = "D:\\misis stuff\\prog\\task_3_6_answer.txt";
                StreamWriter sw = new StreamWriter(output_path, true);

                sw.WriteLine($"The most popular answers to the {name} question:");
                for (int i = 0; i < (dict.Count < 5 ? dict.Count : 5); i++) {
                    sw.WriteLine($"{i + 1}. {keys[i]} - {Math.Round(values[i] / (suma * 1.0) * 100)}%");
                }

                sw.Close();
            }

            printAnswer(first, "first");
            printAnswer(second, "second");
            printAnswer(third, "third");
        }
    }

    class Program {
        static void Main(string[] args) {


            // Level 3 -----------------------
            static void task_3_1() {
                Task_3_1[] groups = new Task_3_1[3];

                string input_path = "D:\\misis stuff\\prog\\task_3_1.txt";
                StreamReader sr = new StreamReader(input_path);

                for (int i = 0; i < 3; i++) {

                    string[] data = sr.ReadLine().Trim().Split();

                    if (data.Length != 6) {
                        Console.WriteLine("Wrong input.");
                        return;
                    }

                    int[] marks = new int[5];
                    for (int j = 1; j < 6; j++) {
                        if (!int.TryParse(data[j], out marks[j - 1])) {
                            Console.WriteLine("Wrong input.");
                            return;
                        }
                        if (marks[j - 1] < 0) {
                            Console.WriteLine("Impossible mark");
                            return;
                        }
                    }

                    groups[i] = new Task_3_1(data[0], marks);
                }

                sr.Close();

                string output_path = "D:\\misis stuff\\prog\\task_3_1_answer.txt";
                StreamWriter sw = new StreamWriter(output_path);

                for (int i = 0; i < 3; i++) {
                    for (int j = i + 1; j < 3; j++) {
                        if (groups[i].AverageMark < groups[j].AverageMark) {
                            Task_3_1 temp = groups[i];
                            groups[i] = groups[j];
                            groups[j] = temp;
                        }
                    }
                }

                sw.WriteLine("Final table:");
                sw.WriteLine("Place\tName\t\tAverage Mark");
                for (int i = 0; i < groups.Length; i++) {
                    sw.WriteLine($"{i + 1}\t{groups[i].Name}\t{groups[i].AverageMark}");
                }

                sw.Close();
            }

            static void task_3_4() {

                static Task_3_4[] getGroup(string groupName) {
                    string input_path = $"D:\\misis stuff\\prog\\task_3_4_group_{groupName}.txt";
                    StreamReader sr = new StreamReader(input_path);
                    
                    if (!int.TryParse(sr.ReadLine().Trim(), out int n)) {
                        return null;
                    }

                    Task_3_4[] group = new Task_3_4[n];

                    for (int i = 0; i < n; i++) {
                        string[] data = sr.ReadLine().Trim().Split();

                        if (data.Length != 2) {
                            return null;
                        }

                        if (!double.TryParse(data[1], out double result)) {
                            return null;
                        }
                        if (result < 0) {
                            return null;
                        }

                        group[i] = new Task_3_4(data[0], result);
                    }

                    sr.Close();

                    return sortGroup(group);
                }

                static Task_3_4[] sortGroup(Task_3_4[] group) {
                    for (int i = 0; i < group.Length; i++) {
                        for (int j = i + 1; j < group.Length; j++) {
                            if (group[i].Result > group[j].Result) {
                                Task_3_4 temp = group[i];
                                group[i] = group[j];
                                group[j] = temp;
                            }
                        }
                    }

                    return group;
                }

                static void printGroup(Task_3_4[] group, string groupName) {
                    string output_path = "D:\\misis stuff\\prog\\task_3_4_answer.txt";
                    StreamWriter sw = new StreamWriter(output_path, true);

                    sw.WriteLine($"Group {groupName} results:");

                    for (int i = 0; i < group.Length; i++) {
                        sw.WriteLine($"{i + 1}. {group[i].Surname} - {group[i].Result}");
                    }
                    sw.WriteLine(" ");
                    sw.Close();
                }

                static Task_3_4[] mixGroups(Task_3_4[] groupA, Task_3_4[] groupB) {
                    Task_3_4[] groupC = new Task_3_4[groupA.Length + groupB.Length];

                    int indA = 0, indB = 0;
                    for (int i = 0; i < groupC.Length; i++) {
                        if (indA == groupA.Length) {
                            groupC[i] = groupB[indB];
                            indB++;
                            continue;
                        }
                        if (indB == groupB.Length) {
                            groupC[i] = groupA[indA];
                            indA++;
                            continue;
                        }
                        if (groupA[indA].Result < groupB[indB].Result) {
                            groupC[i] = groupA[indA];
                            indA++;
                        } else {
                            groupC[i] = groupB[indB];
                            indB++;
                        }
                    }

                    return groupC;
                }


                Task_3_4[] groupA = getGroup("A");

                if (groupA == null) {
                    Console.WriteLine("Wrong input.");
                    return;
                }

                Task_3_4[] groupB = getGroup("B");

                if (groupB == null) {
                    Console.WriteLine("Wrong input.");
                    return;
                }

                printGroup(groupA, "A");
                printGroup(groupB, "B");

                Task_3_4[] groupC = mixGroups(groupA, groupB);

                printGroup(groupC, "A+B");
            }

            static void task_3_6() {
                Task_3_6 data = new Task_3_6();

                string input_path = "D:\\misis stuff\\prog\\task_3_6.txt";
                StreamReader sr = new StreamReader(input_path);
                string answer;

                while ((answer = sr.ReadLine()) != null) {
                    answer = answer.Trim();

                    if (answer == "") {
                        break;
                    }

                    if (answer.Split().Length != 3) {
                        return;
                    }

                    data.addAnswer(answer.Split());
                }
                sr.Close();

                data.printPopularAnswers();
            }

            // -------------------------------

            task_3_6();
        }
    }
}
