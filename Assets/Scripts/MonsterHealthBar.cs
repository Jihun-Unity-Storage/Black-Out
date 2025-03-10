using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthBar : MonoBehaviour
{
    public Image healthBarImage; // Unity UI Image를 사용하여 체력 표시
    private Monster monster; // 몬스터에 대한 참조

    public void SetMonster(Monster monster)
    {
        // 체력 바 스크립트에 몬스터를 설정
        this.monster = monster;
        //Debug.Log(monster.name);
    }

    private void Update()
    {
        if (monster != null)
        {
            //Debug.Log(monster.name);
            // 몬스터의 현재 체력 및 최대 체력 가져오기
            float currentHealth = monster.GetCurrentHealth();
            float maxHealth = monster.GetMaxHealth();
            //Debug.Log(currentHealth);

            // 체력을 FillAmount로 설정 (0에서 1 사이의 값으로 정규화)
            healthBarImage.fillAmount = currentHealth / maxHealth;
        }
    }
}
