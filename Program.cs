using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace graph
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree bt = new BinaryTree();
            int[] arr = new int[]{10,3,45,25,2,6,8};
            for(int i=0;i<arr.Length;i++)
            {
                bt.Add(arr[i]);
            }
            Console.WriteLine(bt.PrintInOrder());
            Console.WriteLine("BFS = {0}",bt.PrintBFS()); 
            Console.WriteLine("ZigZag = {0}", bt.PrintZigZag());
            

            BinaryFormatter bf = new BinaryFormatter();                        
            //MemoryStream ms = new MemoryStream();
            FileStream ms = File.Open("graph.txt", FileMode.OpenOrCreate);
            bf.Serialize(ms, bt);
            
            BinaryFormatter dbf = new BinaryFormatter();
            ms.Seek(0,0);
            BinaryTree dbt = (BinaryTree)dbf.Deserialize(ms)  ;
            Console.WriteLine("Deserialized Binary Tree == {0}",dbt.PrintInOrder());
            Console.WriteLine("Deserialized BFS = {0}",dbt.PrintBFS());
            ms.Close();
            
        }
    }
}
