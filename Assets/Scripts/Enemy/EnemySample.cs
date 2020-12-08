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
        _droprateTable.AddDrop(new ItemEmerald(1), 5);
        _droprateTable.AddDrop(new ItemSilver(3), 45);
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
