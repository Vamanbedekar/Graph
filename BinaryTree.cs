using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[Serializable]
class Node: ISerializable
{
        public Node(int? data)
        {
            this.Data = data;
            if(this.Data.HasValue)
                this.IsValueSet = true;
            this.Left = null;
            this.Right = null;
        }

        public Node(SerializationInfo info, StreamingContext context)
        {
            this.IsValueSet = (bool)info.GetValue("ivs", typeof(bool));
            if(this.IsValueSet)
                this.Data = (int) info.GetValue(nodeSerializationId, typeof(int));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ivs", this.IsValueSet, typeof(bool)); 
            if(this.IsValueSet)
                info.AddValue(nodeSerializationId, this.Data, typeof(int));            
        }
        private bool IsValueSet{get;set;} = false;
        public int? Data {get;set;}
        public Node Left {get;set;}
        public Node Right {get;set;}
        static string nodeSerializationId = "Data";
}
[Serializable]
class BinaryTree: ISerializable
{   
    private Node Root {get;set;} = null;
    static string nodeItemString = "N";
    public BinaryTree(){}
    public BinaryTree(SerializationInfo info, StreamingContext context)
    {
        var siEnum = info.GetEnumerator();        
        siEnum.MoveNext();
              
        Node n =siEnum.Current.Value as Node;               
        siEnum.MoveNext();
        if(!n.Data.HasValue) 
        {
            this.Root = null;
            return;
        }
        this.Root = n;
        Queue q = new Queue();
        q.Enqueue(n);        
        while(q.Count > 0)
        {            
            Node left =siEnum.Current.Value as Node;               
            siEnum.MoveNext();
            Node right = siEnum.Current.Value as Node;
            siEnum.MoveNext();
            Node stackTop = q.Dequeue() as Node;
            if(left.Data.HasValue) 
            {
                stackTop.Left = left;
                q.Enqueue(left);                
            }
            if(right.Data.HasValue) 
            {
                stackTop.Right = right;
                q.Enqueue(right);                
            }
        }
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {        
        Queue q = new Queue();
        if (this.Root == null) return;
        q.Enqueue(this.Root);
        int iter = 0;
        while(q.Count > 0)
        {                      
            Node n = q.Dequeue() as Node;     
            info.AddValue(iter++.ToString(), n, typeof(Node));                
            if(!n.Data.HasValue) continue;
            
            if(n.Left == null)
            {                
                q.Enqueue(new Node(null));
            }
            else
            {
                q.Enqueue(n.Left);
            }

            if(n.Right == null)
            {                
                q.Enqueue(new Node(null));
            }
            else
            {
                q.Enqueue(n.Right);
            }
        }
    }
    public void Add(int i)
    {
        var n = new Node(i);
        if(this.Root == null) 
        {
            this.Root = n;
            return;
        }        
        this.Add(this.Root, n);
    }

    private void Add(Node root, Node toAdd)
    {
        if(toAdd.Data > root.Data)
        {
            if(root.Right == null)
            {
                root.Right = toAdd;
            }
            else
            {
                Add(root.Right, toAdd);
            }
        }
        else
        {
            if(root.Left  == null)
            {
                root.Left = toAdd;
            }
            else
            {
                Add(root.Left, toAdd);
            }
        }
    }
    public bool Remove(int i)
    {
        return false;
    }

    public bool Find(int i)
    {
        return false;
    }

    public string PrintInOrder()
    {        
        string ans = "";
        PrintInOrder(this.Root, ref ans );        
        return ans;
    }    
    private void PrintInOrder(Node root, ref string data)
    {
        if(root == null) return;
        PrintInOrder(root.Left, ref data);
        data += root.Data + ", ";
        PrintInOrder(root.Right, ref data);
    }

    public void PrintPreOrder()
    {

    }

    public void PrintPostOrder()
    {

    }

    public string PrintBFS()
    {
        string str = "";
        Queue q = new Queue();
        if (this.Root == null) return str;
        q.Enqueue(this.Root);
        
        while(q.Count > 0)
        {            
            Node n = q.Dequeue() as Node;                     
            if(!n.Data.HasValue) continue;
            else str += ";" + n.Data.ToString();
            if(n.Left == null)
            {                
                q.Enqueue(new Node(null));
            }
            else
            {
                q.Enqueue(n.Left);
            }

            if(n.Right == null)
            {                
                q.Enqueue(new Node(null));
            }
            else
            {
                q.Enqueue(n.Right);
            }
        }        
        return str;

    }

    public string PrintZigZag()
    {
        string ans = "";
        Stack s = new Stack();
        Queue q = new Queue();
        if(this.Root == null) return ans;
        bool flag = false;
        s.Push(this.Root);
        while(s.Count > 0 || q.Count > 0)
        {
            if(flag)
            {
                while(s.Count > 0)
                {
                    Node sn = s.Pop() as Node;
                    ans += ";"+sn.Data.ToString();
                    if(sn.Left != null && sn.Left.Data.HasValue) q.Enqueue(sn.Left);
                    if(sn.Right != null && sn.Right.Data.HasValue) q.Enqueue(sn.Right);
                }
            }
            else
            {
                while(q.Count >0)
                {
                    Node qn = q.Dequeue() as Node;
                    ans += ";" + qn.Data.ToString();
                    if(qn.Left !=null && qn.Left.Data.HasValue) s.Push(qn.Left);
                    if(qn.Right != null && qn.Right.Data.HasValue) s.Push(qn.Right);
                }
            }
            flag = !flag;
        }
        return ans;
    }
}