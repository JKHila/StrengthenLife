public class ItemClass{
    public int code;
    public string kind;
	public int rank;
	public string name;
	public int rarity;
	public int attakPoint;
    public string explain;
	public int stengthen;

    public ItemClass Clone(){
        ItemClass tp = new ItemClass();
        tp.code = this.code;
        tp.kind = this.kind;
        tp.rank = this.rank;
        tp.name = this.name;
        tp.explain = this.explain;
        tp.attakPoint = this.attakPoint;
        return tp;
    }
}