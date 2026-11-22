using UnityEngine;

public class playerLoadout : MonoBehaviour
{
    [SerializeField] PlayerStats stats;


    private float appliedGroundBonusHP;
    private float appliedWindBonusMS;
    [SerializeField] Element[] equipped; //temporary
    private int activeIndex = 0;   //temporary
    public Element ActiveElement => (equipped != null && activeIndex < equipped.Length) ? equipped[activeIndex] : Element.None;

    void Start() => RefreshPassives();

    public void RefreshPassives()
    {
        float hp = 0f, speed = 0f;

        foreach (Element e in equipped)
        {
            switch (e)
            {

                case Element.Ground: hp += 50f; break;
                case Element.Wind: speed += 10f; break;
            }
        }

        stats.maxHP += hp - appliedGroundBonusHP;
        stats.moveSpeed += speed - appliedWindBonusMS;
        appliedGroundBonusHP = hp;
        appliedWindBonusMS = speed;

        stats.NotifyChanged();
    }
}
