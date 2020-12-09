using Enemy;
using Item;
using Item.Items;
using UnityEngine;

public class EnemySample : EnemyBase
{
    private float startY;
    private static DroprateTable _droprateTable;
    public int speed = 5;

    public override int Damage => 1;
    public override DroprateTable DroprateTable => _droprateTable;

    public void Start() {
        Initialize();
        startY = transform.position.y;
    }

    public static void Initialize() {
        if (_droprateTable != null)
            return;
        
        _droprateTable = new DroprateTable();
        _droprateTable.AddDrop(new ItemEmerald(1), 10);
        _droprateTable.AddDrop(new ItemRuby(1), 10);
        _droprateTable.AddDrop(new ItemDiamond(1), 10);
        _droprateTable.AddDrop(new ItemSilver(1), 20);
        _droprateTable.AddDrop(new ItemSilver(2), 25);
        _droprateTable.AddDrop(new ItemMagicalPowder(2), 20);
        _droprateTable.AddDrop(new ItemMagicalPowder(3), 10);
        _droprateTable.AddDrop(new ItemApple(4), 5);
        _droprateTable.AddDrop(new ItemSilverKey(7), 7);
    }

    public void Update() {
        base.Update();
        
        // Moving towards player
        Quaternion playerRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, Time.deltaTime * 5);
        transform.position += transform.forward * Time.deltaTime * speed;

        // Simple floating effect
        // transform.position = new Vector3(transform.position.x, startY, transform.position.z);
    }
}
