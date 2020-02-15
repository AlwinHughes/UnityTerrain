public class LineConstraint {

  public float start;
  public float end;

  public bool[] used = { false, false};

  public LineConstraint(float start, float end) {
    used[0] = true;
    used[1] = true;
    this.start = start;
    this.end = end;
  }

  public LineConstraint(float start_or_end, bool[] used) {
  this.used = used;
  if(this.used[0])
    this.start = start_or_end;
  else
    this.end = start_or_end;
  }
}
