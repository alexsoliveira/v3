export class UsuarioRegistro {
  nome: string;
  nomeSocial: string;
  idGenero:number;
  idTipoDocumento:number;
  documento:string;
  rg:string;
  email: string;
  senha: string;
  senhaConfirmacao: string;
  termoAceito: boolean;
}

export class UsuarioLogin {
  email: string;
  senha: string;
}

export class UsuarioRespostaLogin {
  accessToken: string;
  expireIn: number;
  usuarioToken: UsuarioToken;
  contaAtivada: boolean;
  idUsuario: number;
}

export class UsuarioToken {
  id: string;
  email: string;
  claims: Array<UsuarioClaim>;
}

export class UsuarioClaim {
  value: string;
  type: string;
}

export class UsuarioConta {
  Email: string;
}

export class UsuarioConfirmaConta {
  userId: string;
  code: string;
}

export class UsuarioResetSenha {
  userId: string;
  code: string;
  senha: string;
}

export class UsuarioAlterarSenha {
  userId: string;
  senhaAtual: string;
  novaSenha: string;
}
