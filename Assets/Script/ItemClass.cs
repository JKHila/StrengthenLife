public class ItemClass{
    public string code;
    public string kind;
	public int rank;
	public string name;
	public int rarity;
    public long baseAttackPoint;
	public long attackPoint;
    public string explain;
    public long cost;
	public int strengthen;
    public int level;
    public int seq;
    
    public int StrengthenItem(){
        if(strengthen < 13){
            attackPoint += rank*rank+baseAttackPoint/2;
            strengthen++;
            return 1;
        }else if(strengthen < 15){
            attackPoint += rank*rank+baseAttackPoint;
            strengthen++;
            return 1;
        }else{
            return 0;
        }
    }
    public ItemClass Clone(){
        ItemClass tp = new ItemClass();
        tp.code = this.code;
        tp.kind = this.kind;
        tp.rank = this.rank;
        tp.name = this.name;
        tp.explain = this.explain;
        tp.baseAttackPoint = this.baseAttackPoint;
        tp.level = 1;
        return tp;
    }
}