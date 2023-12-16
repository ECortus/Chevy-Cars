using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarSpawner : MonoBehaviour
{
    public void Spawn()
    {
        PlayerCarCollection cars = Resources.Load("PRIZES/COLLECTIONS/CarCollection") as PlayerCarCollection;
        PlayerSkinCollection skins = Resources.Load("PRIZES/COLLECTIONS/SkinCollection") as PlayerSkinCollection;
        
        if (cars)
        {
            PlayerCarCollection.Car car = cars.GetCar(PlayerIndex.Car);
            PlayerController player = Instantiate(car.Prefab, transform.parent);

            int hp = car.HPBonus;
            float spd = car.SPDBonus;

            if (skins)
            {
                PlayerSkinCollection.Skin skin = skins.GetSkin(PlayerIndex.Skin);
                hp += skin.HPBonus;
                spd += skin.SPDBonus;
            }
            
            player.SetBonus(hp, spd);
        }
    }
}
