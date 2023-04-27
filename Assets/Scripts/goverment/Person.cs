using System.Collections;
using System.Collections.Generic;

public class Person
{
    public static int lastid = 0;
    public int id { get; set; }
    public Building Home{ get; set; }
    public Building WorkPlace { get; set; }
    public Person() {
        id = ++lastid;
    }
}
