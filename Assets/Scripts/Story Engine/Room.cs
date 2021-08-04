[System.Serializable]
public enum Room
{
    DiningHall,
    Kitchen,
    Garden,
    Attic,
    MasterBedroom,
    Library,
    Study,
    WineCellar
}

public static class RoomUtils
{ 
    public static string GetDescription(Room room)
    {
        switch(room)
        {
            case Room.DiningHall:
                return "Dining Hall";
            case Room.MasterBedroom:
                return "Master Bedroom";
            case Room.WineCellar:
                return "Wine Cellar";
            default:
                return room.ToString();
        }
    }
}
