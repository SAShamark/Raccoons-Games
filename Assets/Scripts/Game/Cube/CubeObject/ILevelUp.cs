namespace Game.Cube.CubeObject
{
    public interface ILevelUp
    {
        public int Number { get; }
        public int ColorIndex { get; }
        public void NextLevel(int nextCubeNumber, int nextColorIndex);
    }
}