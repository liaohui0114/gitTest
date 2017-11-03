using UnityEngine;
using System.Collections;

public class GameTest: MonoBehaviour {
 
    public JoystickController MoveJs;
 
    private Stickman player;
    private Stickman enemy;
    private bool inBattle = false;

    // Use this for initialization
    void Start () {

        player = Stickman.Create("fsq", CharFactory.Instance.Create("char_0001"));
        enemy = Stickman.Create("enemy", CharFactory.Instance.Create("char_0001"));
    }

    void Update()
    {
        if(!inBattle)
        {
           
            player.Dress(EquipmentFactory.Instance.Create("head_f0000"));
            //player.Dress(EquipmentFactory.Instance.Create("head_e0001"));
            player.Dress(EquipmentFactory.Instance.Create("spine_f0000"));
            player.Dress(EquipmentFactory.Instance.Create("downArmL_f0000"));
            player.Dress(EquipmentFactory.Instance.Create("downArmR_f0000"));
            player.Dress(EquipmentFactory.Instance.Create("downLegL_f0000"));
            player.Dress(EquipmentFactory.Instance.Create("downLegR_f0000"));
            player.Dress(EquipmentFactory.Instance.Create("upArmR_f0000"));
            player.Dress(EquipmentFactory.Instance.Create("upArmL_f0000"));
            player.Dress(EquipmentFactory.Instance.Create("upLegL_f0000"));
            player.Dress(EquipmentFactory.Instance.Create("upLegR_f0000"));


            player.Dress(EquipmentFactory.Instance.Create("downArmL_e0001"));
            player.Dress(EquipmentFactory.Instance.Create("downArmR_e0001"));
            player.Dress(EquipmentFactory.Instance.Create("downLegL_e0001"));
            player.Dress(EquipmentFactory.Instance.Create("downLegR_e0001"));
            player.Dress(EquipmentFactory.Instance.Create("head_e0001"));

            //player.Arm(WeaponFactory.Instance.Create("w0001"));
            player.SpawnPostion = new Vector2(-15f, 0f);
            player.IsPlayer = true;



            enemy.Dress(EquipmentFactory.Instance.Create("head_f0000"));
            enemy.Dress(EquipmentFactory.Instance.Create("spine_f0000"));
            enemy.Dress(EquipmentFactory.Instance.Create("downArmL_f0000"));
            enemy.Dress(EquipmentFactory.Instance.Create("downArmR_f0000"));
            enemy.Dress(EquipmentFactory.Instance.Create("downLegL_f0000"));
            enemy.Dress(EquipmentFactory.Instance.Create("downLegR_f0000"));
            enemy.Dress(EquipmentFactory.Instance.Create("upArmR_f0000"));
            enemy.Dress(EquipmentFactory.Instance.Create("upArmL_f0000"));
            enemy.Dress(EquipmentFactory.Instance.Create("upLegL_f0000"));
            enemy.Dress(EquipmentFactory.Instance.Create("upLegR_f0000"));

            enemy.Dress(EquipmentFactory.Instance.Create("downArmL_e0001"));
            enemy.Dress(EquipmentFactory.Instance.Create("downArmR_e0001"));
            enemy.Dress(EquipmentFactory.Instance.Create("downLegL_e0001"));
            enemy.Dress(EquipmentFactory.Instance.Create("downLegR_e0001"));
            enemy.Dress(EquipmentFactory.Instance.Create("head_e0001"));

            //enemy.Arm(WeaponFactory.Instance.Create("w0001"));
            enemy.SpawnPostion = new Vector2(15f, 0f);

            inBattle = true;
        }


    }


	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKeyDown(KeyCode.D))
            player.Undress(PartName.Head,true);

        if(inBattle)
        {
            player.Move(new Vector2(MoveJs.TouchPosition.x, MoveJs.TouchPosition.y));
            Vector2 diff = player.Postion- enemy.Postion;
            if(diff.magnitude > 20f)
            {
                diff = diff.normalized;
                enemy.Move(new Vector2(diff.x * 1.0f, diff.y * 1.0f));
            }
        }    
            

    }


    void LateUpdate()
    {
        if (inBattle)
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(player.Postion.x, player.Postion.y, Camera.main.transform.position.z),0.1f); 
    }
}
