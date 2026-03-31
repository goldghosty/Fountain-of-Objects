using Microsoft.VisualBasic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;

FountainOfObjectsGame game = new FountainOfObjectsGame();
game.Menu();
game.Rounds();




public class FountainOfObjectsGame : Map
{
    public string? playerInput;

    public bool hasWon = false;
    public bool isAlive = true;

    public DateTime startTime = DateTime.Now;
    public DateTime finishedLocal;


    public void Rounds()
    {

        while (hasWon == false && isAlive == true)
        {
            GetLocation(Row, Column);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"You are in the room at (Row:{Row}, Column={Column}).");
            GetRoomType(Row, Column);
            Adjacent(); 
            GetRoomInfo();
            GameOverCheck();
            if (hasWon == true || isAlive == false)
            {
                finishedLocal = DateTime.Now;
                TimeSpan timeSpan = finishedLocal - startTime;
                Console.WriteLine($"It took you {timeSpan} to finish the game.");
                break;
            }
            PlayerCommand();
            


        }
      
    }
    
    public void Menu()
    {
        Console.WriteLine("You enter the Cavern of Objects, a maze of rooms filled with dangerous obstacles in search of the Fountain of Objects.");
        Console.WriteLine("Light is visible only in the entrance, and no other light is seen anywhere in the caverns.");
        Console.WriteLine("You must navigate the cavern through your other senses.");
        Console.WriteLine("Find the fountain of objects, activate it, and return to the entrance.\n");
        Console.WriteLine("Look out for pits. You will feel a breeze is a pit is in an adjacent room. If you enter a room with a pit, you will die.");
        Console.WriteLine("Amaroks roam the caverns. Encountering one is certain death, but you can smell their rotten stench in nearby rooms.");
        Console.WriteLine("Find the fountain of objects, activate it, and return to the entrance.\n");
        Console.WriteLine("For a list of available commands, enter 'help'.\n");
    }

    public void GameOverCheck()
    {
        
        if (FountainOn == true && RoomType == "Entrance")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Congratulations! You have reactivated the Fountain of Objects and returned to the cavern entrance!");

            hasWon = true;

        }
 
        if (RoomType == "Amarok")
        {
            isAlive = false;
        }

        if (RoomType == "Pit")
        {
            isAlive = false;
        }
    }

    public void PlayerCommand()
    {

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("What do you want to do?");
        playerInput = Console.ReadLine();

        if (playerInput == "activate fountain" && RoomType == "Fountain")
        {
            FountainOn = true;
        }
        else if (playerInput == "move north" && Row >= 1)
        {
            Row -= 1;
        }
        else if (playerInput == "move south" && Row <= 3)
        {
            Row += 1;
        }
        else if (playerInput == "move west" && Column >= 1)
        {
            Column -= 1;
        }
        else if (playerInput == "move east" && Column <= 3)
        {
            Column += 1;
        }
        else if (playerInput == "help")
        {
            Console.WriteLine("Available commands are: move north, move south, move east, move west, activate fountain (only available in fountain room).");
        }
        else
        {
            Console.WriteLine("You cannot do that.");
        }

    }

    
}
   

public class Map
{

    public int Row { get; set; }
    public int Column { get; set; }
    
    public string RoomType { get; set; }
    public string? AdjacentRoom { get; set; }
    public bool FountainOn { get; set; }


    public Map ()
    {
        Row = 0;
        Column = 0;
        RoomType = "Entrance";
        FountainOn = false;
    }

    

    public void GetLocation(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public void GetRoomInfo()
    {
        if (RoomType == "Fountain" && FountainOn == true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("You hear the rushing waters from the Fountain of Objects. It has been reactivated!");
        }
        else if (AdjacentRoom == "amarok")
        {

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("You can smell an Amarok nearby, stay sharp!");
        }
        else if (AdjacentRoom == "pit")
        {

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("You feel a draft of air; there is a pit in a nearby room");
        }
        else if (RoomType == "Entrance")
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You see light coming from the cavern entrance");
        }
        
        else if (RoomType == "Fountain" && FountainOn == false)
        {

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("You hear water dripping in this room. The Fountain of Objects is here!");
        }
        else if (RoomType == "Amarok")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You have met your death at the hands of an Amarok. GAME OVER.");
        }
        else if (RoomType == "Pit")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You have fallen into a bottomless pit. GAME OVER.");
        }
        
        else
        {
            Console.WriteLine("You can sense nothing here");
        }

        }

    public void GetRoomType(int row, int column)
    {
        if (Row == 0 && Column == 0) RoomType = "Entrance";  

        else if (Row == 0 && Column == 2) RoomType = "Fountain";
       
        else if (Row < 0 || Row > 3 || Column < 0 || Column > 3) RoomType = "Off Grid";
       
        else if (Row == 1 && Column == 3) RoomType = "Amarok";
       
        else if (Row == 2 && Column == 1) RoomType = "Pit";
        
        else RoomType = "Normal";
        
    }

    public void Adjacent()
    {
        if ((Row == 1 && Column == 1) || (Row == 2 && Column == 0) || (Row == 2 && Column == 2) || (Row == 3 && Column == 1))
        {
            AdjacentRoom = "pit";
        }
        else if ((Row == 0 && Column == 3) || (Row == 1 && Column == 2) || (Row == 2 && Column == 3))
        {
            AdjacentRoom = "amarok";
        }
        else AdjacentRoom = "";
    }

}


