[System.Serializable]
public struct ItemStack
{
    public ItemType ItemType;
    public int Amount;

    public ItemStack(ItemType itemType, int amount)
    {
        ItemType = itemType;
        Amount = amount;
    }

    public static bool operator ==(ItemStack aStack, ItemStack bStack) => aStack.Amount == bStack.Amount;
    public static bool operator !=(ItemStack aStack, ItemStack bStack) => aStack.Amount != bStack.Amount;

    public static bool operator >(ItemStack aStack, ItemStack bStack) => aStack.Amount > bStack.Amount;
    public static bool operator <(ItemStack aStack, ItemStack bStack) => aStack.Amount < bStack.Amount;

    public static bool operator >=(ItemStack aStack, ItemStack bStack) => aStack.Amount >= bStack.Amount;
    public static bool operator <=(ItemStack aStack, ItemStack bStack) => aStack.Amount <= bStack.Amount;

    public static ItemStack operator +(ItemStack aStack, ItemStack bStack) => new ItemStack(aStack.ItemType, aStack.Amount + bStack.Amount);
    public static ItemStack operator -(ItemStack aStack, ItemStack bStack) => new ItemStack(aStack.ItemType, aStack.Amount - bStack.Amount);

    public static ItemStack operator *(ItemStack aStack, ItemStack bStack) => new ItemStack(aStack.ItemType, aStack.Amount * bStack.Amount);

    public static ItemStack operator ++(ItemStack aStack) => new ItemStack(aStack.ItemType, aStack.Amount + 1);
    public static ItemStack operator --(ItemStack aStack) => new ItemStack(aStack.ItemType, aStack.Amount - 1);

    public static bool operator ==(ItemStack aStack, int bAmount) => aStack.Amount == bAmount;
    public static bool operator !=(ItemStack aStack, int bAmount) => aStack.Amount != bAmount;

    public static bool operator >(ItemStack aStack, int bAmount) => aStack.Amount > bAmount;
    public static bool operator <(ItemStack aStack, int bAmount) => aStack.Amount < bAmount;

    public static bool operator >=(ItemStack aStack, int bAmount) => aStack.Amount >= bAmount;
    public static bool operator <=(ItemStack aStack, int bAmount) => aStack.Amount <= bAmount;

    public static ItemStack operator +(ItemStack aStack, int bAmount) => new ItemStack(aStack.ItemType, aStack.Amount + bAmount);
    public static ItemStack operator -(ItemStack aStack, int bAmount) => new ItemStack(aStack.ItemType, aStack.Amount - bAmount);

    public static ItemStack operator *(ItemStack aStack, int bAmount) => new ItemStack(aStack.ItemType, aStack.Amount * bAmount);

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return ItemType.ToString();
    }
}
