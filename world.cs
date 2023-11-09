using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class world : MonoBehaviour
{
    [Header("The World")]
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    public int meter;
    
    
    [Header("Road")]
    [SerializeField] GameObject[] path;
    [SerializeField] GameObject[] surroundingsLeft;
    [SerializeField] GameObject[] surroundingsRight;
    [SerializeField] Transform [] leftSpawn;
    [SerializeField] Transform [] rightSpawn;

    [SerializeField] GameObject empty;
    [Header("Courses")]
    [SerializeField] GameObject[] begin_0;
    [SerializeField] GameObject[] begin_1;
    [SerializeField] GameObject[] begin_2;
    [SerializeField] GameObject[] later_0;
    [SerializeField] GameObject[] later_1;

    obsHandler ObsHandler;
    [SerializeField]

    villageWorld VillageWorld;

    private float range = 0;
    public int itemRan = 0;

    private int blockType = 0;
    private int alpha;
    private Vector3 way;
    private float length = 8f;
    private float itemsLength = 16f;
    private int[] firstCases = { 0, 1, 2, 3};
    private int[] cases = { 0, 1};
    private int[] caseFin = { 0, 1, 2};
    private string[] cases1 = { "A", "B", "C", "F"};
    
    Vector3 rotation = new Vector3(0, 90, 0);

    public List<string> CaseNum = new List<string>();

    void Awake()
    {
        VillageWorld = GameObject.Find("Village").GetComponent<villageWorld>();
        ObsHandler = GameObject.Find("gameManager").GetComponent<obsHandler>();
    }
    void Start()
    {
        AdjustVillageZone();

        firstCase = Random.Range(0, firstCases.Length);
        Case0 = Random.Range(0, cases.Length);

        for (int i = 0; i < cases1.Length; i++)
        {
            CaseNum.Add(cases1[i]);
        }
        int index = Random.Range(0, CaseNum.Count-1);
        Case1 = CaseNum[index];

        CaseFinal = Random.Range(0, caseFin.Length);

        Vector3 next = end.position;
        Vector3 nextA = end.position;
        
        float total = Vector3.Distance(start.position, end.position);
        way = Vector3.back;        
        for (int alpha = 0; alpha < 22; alpha++)
        {
            GameObject newBlock = Path(next, way);
            next += -way * length;
            if( alpha > 5 && alpha < 12)
            {
                GameObject items = Course (nextA, way);
                itemRan++;
            }
            nextA += -way * itemsLength;
            alpha += 1;
            range++;
        }

    }   
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != null)
        {
            if (other.gameObject.tag == "Ground")
            {
                GameObject newBlock = Path(other.transform.position, way);
                Vector3 nextPosition = -length * way;
                newBlock.transform.position += nextPosition;
                range++;
                meter++;
            }

            if(other.gameObject.tag == "Items")
            {
                GameObject items = Course(other.transform.position, way);
                Vector3 nextPositionA = -itemsLength * way;
                items.transform.position += nextPositionA;
                itemRan++;
            }
        }
    }
    private int[] villageStartRan = { 1, 11 };
    private int villageStart = 1;
    private int villageZone = 30;

    private void AdjustVillageZone()
    {
        int pick = Random.Range(0, villageStartRan.Length-1);
        villageStart = villageStartRan[pick];
    }

    GameObject Path(Vector3 position, Vector3 moveDir)
    {      

        if (meter > villageStart && meter < villageStart + villageZone) { blockType = 1; }
        else { blockType = 0; }

        GameObject block = path[blockType];
        GameObject newBlock = Instantiate(block);
        newBlock.transform.position = position;
        mover mover = newBlock.GetComponent<mover>();
        if (mover != null)
        {           
            mover.SetDestination(end.position);
        }

        if (blockType == 0) { Subs_0(position, newBlock); ItemRangeControl_1(); }  
        if (blockType == 1) { Subs_1(position, newBlock); ItemRangeControl_0(); rangeControlInt = 1; }  

        return newBlock;
    }

    private int rangeControlInt=0;
    private void ItemRangeControl_0()
    {
        if(rangeControlInt == 0) { itemRan = 0; }
    }
    private void ItemRangeControl_1()
    {
        if (rangeControlInt != 0) { itemRan = 0; rangeControlInt = 0; }
    }
    GameObject Course (Vector3 positionA, Vector3 moveDir)
    {          
        GameObject itm = Instantiate(empty);
        mover mover = itm.GetComponent<mover>();
        if (mover != null)
        {
            mover.SetDestination(end.position);
        }
        if(blockType == 0) { TheItems(positionA, itm); }
        if(blockType == 1) { VillageWorld.TheVillageCourse(positionA, itm); }

        return itm;
    }

    private void TheItems(Vector3 positionA, GameObject itm)
    {
        
        if (Phase <= 1)
        {
            if (firstCase == 0)
            {
                if (Case0 == 0 )
                {
                    if (itemRan == 0) { Begin_0(positionA, itm, 0); }
                    if (itemRan == 1) { Begin_0(positionA, itm, 4); }
                    if (itemRan == 2) { Begin_0(positionA, itm, 0); }
                    if (itemRan == 3) { Begin_0(positionA, itm, 4); }
                    if (itemRan == 4) { Begin_0(positionA, itm, 2); }
                    if (itemRan == 5) { Begin_0(positionA, itm, 8); }
                    if (itemRan == 6) { Begin_0(positionA, itm, 3); }
                    if (itemRan == 7) { Begin_0(positionA, itm, 4); }
                    if (itemRan >= 8 && itemRan < 10) { Begin_0(positionA, itm, 5); }
                    if (itemRan == 10) { Begin_0(positionA, itm, 4); }
                    if (itemRan == 11) { Begin_0(positionA, itm, 6); }
                    if (itemRan == 12) { Begin_0(positionA, itm, 6); }
                    if (itemRan == 13) { Begin_0(positionA, itm, 4); }
                    if (itemRan == 14) { Begin_0(positionA, itm, 8); }
                    if (itemRan == 15) { Begin_0(positionA, itm, 5); }
                    if (itemRan == 16) { Begin_0(positionA, itm, 5); }
                    if (itemRan == 17) { Begin_0(positionA, itm, 3); }
                    if (itemRan == 18) { Begin_0(positionA, itm, 4); }
                    if (itemRan == 19) { Begin_0(positionA, itm, 6); }
                    if (itemRan == 20) { Begin_0(positionA, itm, 4); }
                    if (itemRan == 21)
                    {
                        Begin_0(positionA, itm, 4);
                        Case0 = 1; itemRan = -1; Phase++;
                    }
                }
                else if (Case0 == 1)
                {
                    if (itemRan == 0) { Begin_0(positionA, itm, 0); }
                    if (itemRan == 1) { Begin_0(positionA, itm, 4); }
                    if (itemRan == 2) { Begin_0(positionA, itm, 0); }
                    if (itemRan == 3) { Begin_0(positionA, itm, 4); }
                    if (itemRan == 4) { Begin_0(positionA, itm, 2); }
                    if (itemRan == 5) { Begin_0(positionA, itm, 8); }
                    if (itemRan == 6) { Begin_0(positionA, itm, 3); }
                    if (itemRan == 7) { Begin_0(positionA, itm, 4); }
                    if (itemRan >= 8 && itemRan < 10) { Begin_0(positionA, itm, 5); }
                    if (itemRan == 10) { Begin_0(positionA, itm, 4); }
                    if (itemRan == 11) { Begin_0(positionA, itm, 6); }
                    if (itemRan == 12) { Begin_0(positionA, itm, 6); }
                    if (itemRan == 13) { Begin_0(positionA, itm, 4); }
                    if (itemRan == 14) { Begin_0(positionA, itm, 8); }
                    if (itemRan == 15) { Begin_0(positionA, itm, 5); }
                    if (itemRan == 16) { Begin_0(positionA, itm, 5); }
                    if (itemRan == 17) { Begin_0(positionA, itm, 3); }
                    if (itemRan == 18) { Begin_0(positionA, itm, 4); }
                    if (itemRan == 19) { Begin_0(positionA, itm, 6); }
                    if (itemRan == 20) { Begin_0(positionA, itm, 4); }
                    if (itemRan == 21)
                    {
                        Begin_0(positionA, itm, 4);
                        Case0 = 0; itemRan = -1; Phase++;
                    }
                }
            }
            else if(firstCase == 1)
            {
                if (Case0 == 0 )
                {
                    if (itemRan == 0) { Begin_1(positionA, itm, 1); }
                    if (itemRan == 1) { Begin_0(positionA, itm, 8); }
                    if (itemRan == 2) { Begin_1(positionA, itm, 1); }
                    if (itemRan == 3) { Begin_0(positionA, itm, 8); }
                    if (itemRan == 4) { Begin_1(positionA, itm, 1); }
                    if (itemRan == 5) { Begin_0(positionA, itm, 8); }
                    if (itemRan == 6) { Begin_1(positionA, itm, 1); }
                    if (itemRan == 7) { Begin_0(positionA, itm, 8); }
                    if (itemRan == 8) { Begin_1(positionA, itm, 1); }
                    if (itemRan == 9) { Begin_0(positionA, itm, 8); }
                    if (itemRan == 10) { Begin_1(positionA, itm, 2); }
                    if (itemRan == 11) { Begin_0(positionA, itm, 8); }
                    if (itemRan == 12) { Begin_1(positionA, itm, 1); }
                    if (itemRan == 13) { Begin_0(positionA, itm, 8); }
                    if (itemRan == 14) { Begin_1(positionA, itm, 1); }
                    if (itemRan == 15) { Begin_0(positionA, itm, 8); }
                    if (itemRan == 16) { Begin_1(positionA, itm, 1); }
                    if (itemRan == 17) { Begin_0(positionA, itm, 8); }
                    if (itemRan == 18) { Begin_1(positionA, itm, 0); 
                        Case0 = 1; itemRan = -1; Phase++; 
                    }

                }
                else if (Case0 == 1 )
                {
                    if (itemRan == 0) { Begin_1(positionA, itm, 2); }
                    if (itemRan == 1) { Begin_1(positionA, itm, 2); }
                    if (itemRan == 2) { Begin_1(positionA, itm, 2); }
                    if (itemRan == 3) { Begin_1(positionA, itm, 2); }
                    if (itemRan == 4) { Begin_1(positionA, itm, 1); }
                    if (itemRan == 5) { Begin_1(positionA, itm, 2); }
                    if (itemRan == 6) { Begin_1(positionA, itm, 2); }
                    if (itemRan == 7) { Begin_1(positionA, itm, 2); }
                    if (itemRan == 8) { Begin_1(positionA, itm, 0); 
                        Case0 = 0; itemRan = -1; Phase++;
                    }           
                }
            }
            else if(firstCase == 2)
            {
                if (Case0 == 0)
                {
                    if (itemRan == 0) { Begin_1(positionA, itm, 3); }
                    if (itemRan == 1) { Begin_1(positionA, itm, 3); }
                    if (itemRan == 2) { Begin_1(positionA, itm, 3); }
                    if (itemRan == 3) { Begin_1(positionA, itm, 3); }
                    if (itemRan == 4) { Begin_1(positionA, itm, 3); }
                    if (itemRan == 5) { Begin_1(positionA, itm, 3); }
                    if (itemRan == 6) { Begin_1(positionA, itm, 3); }
                    if (itemRan == 7) { Begin_1(positionA, itm, 3); }
                    if (itemRan == 8) { Begin_1(positionA, itm, 3); }
                    if (itemRan == 9) { Begin_1(positionA, itm, 3); }
                    if (itemRan == 10) { Begin_1(positionA, itm, 3); }
                    if (itemRan == 11) { Begin_1(positionA, itm, 3); }
                    if (itemRan == 12)
                    {
                        Begin_1(positionA, itm, 0);
                        Case0 = 1; itemRan = -1; Phase++;
                    }
                }
                else if (Case0 == 1)
                {
                    if (itemRan == 0) { Begin_1(positionA, itm, 4); }
                    if (itemRan == 1) { Begin_1(positionA, itm, 4); }
                    if (itemRan == 2) { Begin_1(positionA, itm, 4); }
                    if (itemRan == 3) { Begin_0(positionA, itm, 1); }
                    if (itemRan == 4) { Begin_0(positionA, itm, 8); }
                    if (itemRan == 5) { Begin_0(positionA, itm, 1); }
                    if (itemRan == 6) { Begin_1(positionA, itm, 4); }
                    if (itemRan == 7) { Begin_1(positionA, itm, 4); }
                    if (itemRan == 8) { Begin_1(positionA, itm, 4); }
                    if (itemRan == 9) { Begin_0(positionA, itm, 1); }
                    if (itemRan == 10) { Begin_1(positionA, itm, 4); }
                    if (itemRan == 11) { Begin_1(positionA, itm, 4); }
                    if (itemRan == 12) { Begin_1(positionA, itm, 4); }
                    if (itemRan == 13)
                    {
                        Begin_1(positionA, itm, 0);
                        Case0 = 0; itemRan = -1; Phase++;
                    }
                }
            }
            else if(firstCase == 3)
            {
                if(Case0 == 0 || Case0 == 1)
                {
                    if (itemRan == 0) { Begin_2(positionA, itm, 0); }
                    if (itemRan == 1) { Begin_1(positionA, itm, 0); }
                    if (itemRan == 2) { Begin_2(positionA, itm, 0); }
                    if (itemRan == 3) { Begin_1(positionA, itm, 0); }
                    if (itemRan == 4) { Begin_2(positionA, itm, 0); }
                    if (itemRan == 5) { Begin_1(positionA, itm, 0); }
                    if (itemRan == 6) { Begin_2(positionA, itm, 0); }
                    if (itemRan == 7) { Begin_1(positionA, itm, 0);
                        int[] picks = { 0, 1, 2 };
                        ObsHandler.isBoost = false;
                        firstCase = Random.Range(0, picks.Length);
                        Phase++;
                        itemRan = -1; }
                }
            }
        }
        else if (Phase <= 4 && Phase >1 )
        {
            if (Case1 == "A")
            {

                if (itemRan == 0) { Later_0(positionA, itm, 0); }
                if (itemRan == 1) { Later_0(positionA, itm, 1); }
                if (itemRan == 2) { Later_0(positionA, itm, 0); }
                if (itemRan == 3) { Later_0(positionA, itm, 1); }
                if (itemRan == 4) { Later_0(positionA, itm, 0); }
                if (itemRan == 5) { Begin_0(positionA, itm, 8); }
                if (itemRan == 6) { Later_0(positionA, itm, 1); }
                if (itemRan == 7) { Later_0(positionA, itm, 0); }
                if (itemRan == 8) { Later_0(positionA, itm, 1); }
                if (itemRan == 9) { Later_0(positionA, itm, 2); }
                if (itemRan == 10) { Later_0(positionA, itm, 1); }
                if (itemRan == 11) { Later_0(positionA, itm, 1); }
                if (itemRan == 12) { Later_0(positionA, itm, 0); }
                if (itemRan == 13) { Later_0(positionA, itm, 1); }
                if (itemRan == 14) { Later_0(positionA, itm, 0); }
                if (itemRan == 15) { Later_0(positionA, itm, 1); }
                if (itemRan == 16) { Later_0(positionA, itm, 0); }
                if (itemRan == 17) { Later_0(positionA, itm, 1); }
                if (itemRan == 18) { Later_0(positionA, itm, 0); }
                if (itemRan == 19) { Later_0(positionA, itm, 1); }
                if (itemRan == 20)
                {
                    Later_0(positionA, itm, 1);
                    itemRan = -1; Phase++;
                    CaseNum.Remove(Case1);
                    int index = Random.Range(0, CaseNum.Count-1);
                    if (CaseNum != null)
                    {
                        Case1 = CaseNum[index];
                    }

                }
            }
            else if (Case1 == "B")
            {
                if (itemRan == 0) { Later_0(positionA, itm, 3); }
                if (itemRan == 1) { Begin_0(positionA, itm, 8); }
                if (itemRan == 2) { Later_0(positionA, itm, 3); }
                if (itemRan == 3) { Begin_0(positionA, itm, 8); }
                if (itemRan == 4) { Later_0(positionA, itm, 3); }
                if (itemRan == 5) { Begin_0(positionA, itm, 8); }
                if (itemRan == 6) { Later_0(positionA, itm, 3); }
                if (itemRan == 7) { Begin_0(positionA, itm, 8); }
                if (itemRan == 8) { Later_0(positionA, itm, 3); }
                if (itemRan == 9) { Begin_0(positionA, itm, 8); }
                if (itemRan == 10) { Later_0(positionA, itm, 3); }
                if (itemRan == 11) { Begin_0(positionA, itm, 8); }
                if (itemRan == 12) { Later_0(positionA, itm, 3); }
                if (itemRan == 13) { Begin_0(positionA, itm, 8); }
                if (itemRan == 14) { Later_0(positionA, itm, 3); }
                if (itemRan == 15) { Begin_0(positionA, itm, 8); }
                if (itemRan == 16) { Later_0(positionA, itm, 3); }
                if (itemRan == 17) { Begin_0(positionA, itm, 8); }
                if (itemRan == 18) { Later_0(positionA, itm, 3); }
                if (itemRan == 19)
                {
                    Later_0(positionA, itm, 1);
                    CaseNum.Remove(Case1);
                    itemRan = -1; Phase++;
                    int index = Random.Range(0, CaseNum.Count-1);
                    if (CaseNum != null)
                    {
                        Case1 = CaseNum[index];
                    }
                }
            }
            else if (Case1 == "C")
            {
                if (itemRan == 0) { Later_1(positionA, itm, 0); }
                if (itemRan == 1) { Later_0(positionA, itm, 1); }
                if (itemRan == 2) { Later_1(positionA, itm, 0); }
                if (itemRan == 3) { Later_0(positionA, itm, 1); }
                if (itemRan == 4) { Later_1(positionA, itm, 0); }
                if (itemRan == 5) { Later_0(positionA, itm, 1); }
                if (itemRan == 6) { Later_1(positionA, itm, 0); }
                if (itemRan == 7) { Later_0(positionA, itm, 1); }
                if (itemRan == 8)
                {
                    Begin_1(positionA, itm, 0);
                    ObsHandler.isBoost = false;
                    CaseNum.Remove(Case1);
                    itemRan = -1; Phase++;
                    int index = Random.Range(0, CaseNum.Count-1);
                    if (CaseNum != null)
                    {
                        Case1 = CaseNum[index];
                    }
                }
            }
        }
        else if (Phase > 4)
        {
            if (CaseFinal == 0)
            {
                if (itemRan == 0) { Later_0(positionA, itm, 4); }
                if (itemRan == 1) { Later_0(positionA, itm, 1); }
                if (itemRan == 2) { Later_0(positionA, itm, 1); }
                if (itemRan == 3) { Later_0(positionA, itm, 4); }
                if (itemRan == 4) { Later_0(positionA, itm, 1); }
                if (itemRan == 5) { Later_0(positionA, itm, 1); }
                if (itemRan == 6) { Later_0(positionA, itm, 4); }
                if (itemRan == 7) { Later_0(positionA, itm, 1); }
                if (itemRan == 8) { Later_0(positionA, itm, 1); }
                if (itemRan == 9) { Later_0(positionA, itm, 4); }
                if (itemRan == 10) { Later_0(positionA, itm, 1); }
                if (itemRan == 11) { Later_0(positionA, itm, 1); }
                if (itemRan == 12) { Later_0(positionA, itm, 4); }
                if (itemRan == 13) { Later_0(positionA, itm, 1); }
                if (itemRan == 14) { Later_0(positionA, itm, 1); }
                if (itemRan == 15) { Later_0(positionA, itm, 4); }
                if (itemRan == 16) { Later_0(positionA, itm, 1); }
                if (itemRan == 17) { Later_0(positionA, itm, 1); }
                if (itemRan == 18) { Later_0(positionA, itm, 4); }
                if (itemRan == 19) { Later_0(positionA, itm, 1); }
                if (itemRan == 20) { Later_0(positionA, itm, 1); }
                if (itemRan == 21) { Later_0(positionA, itm, 4); }
                if (itemRan == 22) { Later_0(positionA, itm, 1); }
                if (itemRan == 23) { Later_0(positionA, itm, 1); }
                if (itemRan == 24) { Later_0(positionA, itm, 4); }
                if (itemRan == 25) { Later_0(positionA, itm, 1); }
                if (itemRan == 26) { Later_0(positionA, itm, 1); }
                if (itemRan == 27) { Later_0(positionA, itm, 4); }
                if (itemRan == 28) { Later_0(positionA, itm, 1); }
                if (itemRan == 29) { Later_0(positionA, itm, 1); }
                if (itemRan == 30)
                {
                    Later_0(positionA, itm, 1);
                    itemRan = -1; Phase++;
                    var myCodes = new int[2];
                    myCodes[0] = 1;
                    myCodes[1] = 2;
                    var index = Random.Range(0, myCodes.Length);
                    CaseFinal = myCodes[index];
                }
            }
            else if (CaseFinal == 1)
            {
                if (itemRan == 0) { Later_0(positionA, itm, 5); }
                if (itemRan == 1) { Later_0(positionA, itm, 1); }
                if (itemRan == 2) { Later_0(positionA, itm, 5); }
                if (itemRan == 3) { Later_0(positionA, itm, 1); }
                if (itemRan == 4) { Later_0(positionA, itm, 5); }
                if (itemRan == 5) { Later_0(positionA, itm, 1); }
                if (itemRan == 6) { Later_0(positionA, itm, 5); }
                if (itemRan == 7) { Later_0(positionA, itm, 1); }
                if (itemRan == 8) { Later_0(positionA, itm, 5); }
                if (itemRan == 9) { Later_0(positionA, itm, 1); }
                if (itemRan == 10) { Later_0(positionA, itm, 5); }
                if (itemRan == 11) { Later_0(positionA, itm, 1); }
                if (itemRan == 12) { Later_0(positionA, itm, 5); }
                if (itemRan == 13) { Later_0(positionA, itm, 1); }
                if (itemRan == 14) { Later_0(positionA, itm, 5); }
                if (itemRan == 15) { Later_0(positionA, itm, 1); }
                if (itemRan == 16) { Later_0(positionA, itm, 5); }
                if (itemRan == 17) { Later_0(positionA, itm, 1); }
                if (itemRan == 18) { Later_0(positionA, itm, 5); }
                if (itemRan == 19) { Later_0(positionA, itm, 1); }
                if (itemRan == 20) { Later_0(positionA, itm, 5); }
                if (itemRan == 21) { Later_0(positionA, itm, 1); }
                if (itemRan == 22) { Later_0(positionA, itm, 5); }
                if (itemRan == 23) { Later_0(positionA, itm, 1); }
                if (itemRan == 24) { Later_0(positionA, itm, 5); }
                if (itemRan == 25) { Later_0(positionA, itm, 1); }
                if (itemRan == 26)
                {
                    Later_0(positionA, itm, 1);
                    itemRan = -1; Phase++;
                    var myCodes = new int[2];
                    myCodes[0] = 0;
                    myCodes[1] = 2;
                    var index = Random.Range(0, myCodes.Length);
                    CaseFinal = myCodes[index];
                }
            }
            else if (CaseFinal == 2)
            {
                if (itemRan == 0) { Later_0(positionA, itm, 6); }
                if (itemRan == 1) { Later_0(positionA, itm, 1); }
                if (itemRan == 2) { Later_0(positionA, itm, 1); }
                if (itemRan == 3) { Later_0(positionA, itm, 6); }
                if (itemRan == 4) { Later_0(positionA, itm, 1); }
                if (itemRan == 5) { Later_0(positionA, itm, 1); }
                if (itemRan == 6) { Later_0(positionA, itm, 6); }
                if (itemRan == 7) { Later_0(positionA, itm, 1); }
                if (itemRan == 8) { Later_0(positionA, itm, 1); }
                if (itemRan == 9) { Later_0(positionA, itm, 6); }
                if (itemRan == 10) { Later_0(positionA, itm, 1); }
                if (itemRan == 11) { Later_0(positionA, itm, 1); }
                if (itemRan == 12) { Later_0(positionA, itm, 6); }
                if (itemRan == 13) { Later_0(positionA, itm, 1); }
                if (itemRan == 14) { Later_0(positionA, itm, 1); }
                if (itemRan == 15) { Later_0(positionA, itm, 6); }
                if (itemRan == 16) { Later_0(positionA, itm, 1); }
                if (itemRan == 17) { Later_0(positionA, itm, 1); }
                if (itemRan == 18) { Later_0(positionA, itm, 6); }
                if (itemRan == 19) { Later_0(positionA, itm, 1); }
                if (itemRan == 20)
                {
                    Later_0(positionA, itm, 1);
                    ObsHandler.isAbove = false;
                    itemRan = -1; Phase++;
                    var myCodes = new int[2];
                    myCodes[0] = 0;
                    myCodes[1] = 1;
                    var index = Random.Range(0, myCodes.Length);
                    CaseFinal = myCodes[index];
                }
            }
        }
    }

    public int firstCase;
    public int Case0;

    public string Case1;
    public int CaseFinal;

    public int Phase = 0;

    private void Begin_0(Vector3 positionA, GameObject itm, int num)
    {
        Vector3 spawnLocA = positionA;
        int itemPick = Random.Range(0, begin_0.Length);
        GameObject newItems = Instantiate(begin_0[num], spawnLocA, Quaternion.identity, itm.transform);
    }
    private void Begin_1(Vector3 positionA, GameObject itm, int num)
    {
        Vector3 spawnLocA = positionA;
        int itemPick = Random.Range(0, begin_1.Length);
        GameObject newItems = Instantiate(begin_1[num], spawnLocA, Quaternion.identity, itm.transform);
    }
    private void Begin_2(Vector3 positionA, GameObject itm, int num)
    {
        Vector3 spawnLocA = positionA;
        int itemPick = Random.Range(0, begin_2.Length);
        GameObject newItems = Instantiate(begin_2[num], spawnLocA, Quaternion.identity, itm.transform);
    }

    private void Later_0(Vector3 positionA, GameObject itm, int num)
    {
        Vector3 spawnLocA = positionA;
        int itemPick = Random.Range(0, later_0.Length);
        GameObject newItems = Instantiate(later_0[num], spawnLocA, Quaternion.identity, itm.transform);
    }
    private void Later_1(Vector3 positionA, GameObject itm, int num)
    {
        Vector3 spawnLocA = positionA;
        int itemPick = Random.Range(0, later_1.Length);
        GameObject newItems = Instantiate(later_1[num], spawnLocA, Quaternion.identity, itm.transform);
    }

    private void Subs_0(Vector3 position, GameObject newBlock)
    {     

        if (range < 1)  { ObjectsRight(position, newBlock, rightSpawn, 4); ObjectsLeft(position, newBlock, leftSpawn, 4); }
        if (range == 1) { ObjectsRight(position, newBlock, rightSpawn, 2); ObjectsLeft(position, newBlock, leftSpawn, 1); }
        if (range == 2) { ObjectsRight(position, newBlock, rightSpawn, 0); ObjectsLeft(position, newBlock, leftSpawn, 0); }
        if (range == 3) { ObjectsRight(position, newBlock, rightSpawn, 1); ObjectsLeft(position, newBlock, leftSpawn, 3); }
        if (range == 4) { ObjectsRight(position, newBlock, rightSpawn, 0); ObjectsLeft(position, newBlock, leftSpawn, 0); }
        if (range == 5) { ObjectsRight(position, newBlock, rightSpawn, 3); ObjectsLeft(position, newBlock, leftSpawn, 2); }
        if (range == 6) { range = 0; }
        
    }

    private void Subs_1(Vector3 position, GameObject newBlock)
    {
        ObjectsNone(position, newBlock, rightSpawn);
        ObjectsNone(position, newBlock, leftSpawn);
        range = 1;
    }

    private void ObjectsLeft(Vector3 position, GameObject newBlock, Transform[] Spawns, int num)
    {
        foreach (Transform houseSpawn in Spawns)
        {
            Vector3 spawnLoc = position + (houseSpawn.position - start.position);           
            int picked = Random.Range(0, surroundingsLeft.Length);
            GameObject newHouse = Instantiate(surroundingsLeft[num], spawnLoc, Quaternion.identity, newBlock.transform);           
        }
    }
    private void ObjectsRight(Vector3 position, GameObject newBlock, Transform[] Spawns, int num)
    {
        foreach (Transform houseSpawn in Spawns)
        {
            Vector3 spawnLoc = position + (houseSpawn.position - start.position);  
            int picked = Random.Range(0, surroundingsRight.Length);
            GameObject newHouse = Instantiate(surroundingsRight[num], spawnLoc, Quaternion.identity, newBlock.transform);       
        }
    }
    private void ObjectsNone(Vector3 position, GameObject newBlock, Transform[] Spawns)
    {
        foreach (Transform houseSpawn in Spawns)
        {
            Vector3 spawnLoc = position + (houseSpawn.position - start.position);
            GameObject newHouse = Instantiate(surroundingsLeft[0], spawnLoc, Quaternion.identity, newBlock.transform);
        }
    }


    void Update()
    {
        Debug.Log(villageStart);
    }
}
