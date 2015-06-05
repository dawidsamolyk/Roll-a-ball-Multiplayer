using UnityEngine;
using System.Collections;

public class PlayerEntity
{

	private string nick, points, ping;

	public string Nick {
		get {
			return this.nick;
		}
		set {
			nick = value;
		}
	}

	public string Points {
		get {
			return this.points;
		}
		set {
			points = value;
		}
	}

	public string Ping {
		get {
			return this.ping;
		}
		set {
			ping = value;
		}
	}

	public PlayerEntity (string nick, string points, string ping)
	{
		this.nick = nick;
		this.points = points;
		this.ping = ping;
	}

}
