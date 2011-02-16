namespace Valker.PlayServer
{
    enum Permissions
    {
        None = 0,               // All clients has this permissions
        BroadcastMessage = 1,   // Send message to all clients in the room
        PrivateMessage = 2,     // Send message to concrete client
        ShuttingUp = 3,         // Make client listen-only
        Kick = 4,               // Remove client from the room
        All                     // All permissions (like root in UNIX)
    }
}