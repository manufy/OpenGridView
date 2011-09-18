using System;

[AttributeUsage(AttributeTargets.All)]
public class OpenGridViewAttribute : System.Attribute
{

    //Private fields.
    private bool _sendUserToLastPageAfterInsert;

    //This constructor defines two required parameters: name and level.

    public OpenGridViewAttribute(bool sendusertolastpageafterinsert)
    {
        this._sendUserToLastPageAfterInsert = sendusertolastpageafterinsert;
    }

    /* 
    //Define Name property.
    //This is a read-only attribute.

    public virtual string Name
    {
        get { return name; }
    }

    //Define Level property.
    //This is a read-only attribute.

    public virtual string Level
    {
        get { return level; }
    }

    //Define Reviewed property. 
    //This is a read/write attribute. 

    public virtual bool Reviewed
    {
        get { return reviewed; }
        set { reviewed = value; }
    }
     */

}