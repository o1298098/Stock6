using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Stock6.Models
{
     public class ImageColloection : IEnumerable
    {
        ArrayList al = new ArrayList();

        public Task<List<ImageModel>> Task { get;internal set; }

        public IEnumerator GetEnumerator()
        {
            return new MyGetEnumberater(this.al);
        }
        public ImageModel this[int index]
        {
            get { return (ImageModel)al[index]; }
        }

        public void Add(ImageModel p)
        {
            al.Add(p);
        }

        public void AddRange(ICollection ip)
        {
            al.AddRange(ip);
        }

        public void Clear()
        {
            al.Clear();
        }

        public void Insert(int index, ImageModel value)
        {
            al.Insert(index, value);
        }

        public int IndexOf(ImageModel p)
        {
            return al.IndexOf(p);
        }

        public void RemoveAt(int index)
        {
            al.RemoveAt(index);
        }

        public void Remove(ImageModel p)
        {
            if (al.Contains(p))
            {
                int temp = IndexOf(p);
                RemoveAt(temp);
            }
        }
    }
    public class MyGetEnumberater : IEnumerator
    {
        int i = 0;
        ArrayList ps;

        public MyGetEnumberater(ArrayList p)
        {
            ps = p;
        }

        /// <summary>
        /// 得到当前元素
        /// </summary>
        public object Current
        {
            get { return ps[i++]; }  //注意这里的i++ ，是先运算再自增。
            //相当于
            //object o=ps[i];
            //i++;
            //return o;

        }

        /// <summary>
        /// 移动到下一个元素
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            if (i > ps.Count - 1)
            {
                return false;
            }
            return true;
        }

        //重置
        public void Reset()
        {
            i = 0;
        }
    }
}

