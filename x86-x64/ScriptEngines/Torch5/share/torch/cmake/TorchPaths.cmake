SET(Torch_INSTALL_PREFIX "${Torch_DIR}/../../../")

SET(Torch_INSTALL_BIN_SUBDIR "bin")
SET(Torch_INSTALL_MAN_SUBDIR "share/man")
SET(Torch_INSTALL_LIB_SUBDIR "lib")
SET(Torch_INSTALL_SHARE_SUBDIR "share")
SET(Torch_INSTALL_INCLUDE_SUBDIR "include")
SET(Torch_INSTALL_HLP_SUBDIR "share/torch/hlp")
SET(Torch_INSTALL_HTML_SUBDIR "share/torch/html")
SET(Torch_INSTALL_LUA_PATH_SUBDIR "share/lua/5.1")
SET(Torch_INSTALL_LUA_CPATH_SUBDIR "lib/lua/5.1")

SET(Torch_INSTALL_BIN "${Torch_INSTALL_PREFIX}/${Torch_INSTALL_BIN_SUBDIR}")
SET(Torch_INSTALL_MAN "${Torch_INSTALL_PREFIX}/${Torch_INSTALL_MAN_SUBDIR}")
SET(Torch_INSTALL_LIB "${Torch_INSTALL_PREFIX}/${Torch_INSTALL_LIB_SUBDIR}")
SET(Torch_INSTALL_SHARE "${Torch_INSTALL_PREFIX}/${Torch_INSTALL_SHARE_SUBDIR}")
SET(Torch_INSTALL_INCLUDE "${Torch_INSTALL_PREFIX}/${Torch_INSTALL_INCLUDE_SUBDIR}")
SET(Torch_INSTALL_HLP "${Torch_INSTALL_PREFIX}/${Torch_INSTALL_HLP_SUBDIR}")
SET(Torch_INSTALL_HTML "${Torch_INSTALL_PREFIX}/${Torch_INSTALL_HTML_SUBDIR}")
SET(Torch_INSTALL_CMAKE "${Torch_INSTALL_PREFIX}/${Torch_INSTALL_CMAKE_SUBDIR}")
SET(Torch_INSTALL_LUA_PATH "${Torch_INSTALL_PREFIX}/${Torch_INSTALL_LUA_PATH_SUBDIR}")
SET(Torch_INSTALL_LUA_CPATH "${Torch_INSTALL_PREFIX}/${Torch_INSTALL_LUA_CPATH_SUBDIR}")

FILE(RELATIVE_PATH Torch_INSTALL_BIN_RIDBUS "${Torch_INSTALL_BIN}" "${Torch_INSTALL_PREFIX}/.")
FILE(RELATIVE_PATH Torch_INSTALL_CMAKE_RIDBUS "${Torch_INSTALL_CMAKE}" "${Torch_INSTALL_PREFIX}/.")
GET_FILENAME_COMPONENT(Torch_INSTALL_BIN_RIDBUS "${Torch_INSTALL_BIN_RIDBUS}" PATH)
GET_FILENAME_COMPONENT(Torch_INSTALL_CMAKE_RIDBUS "${Torch_INSTALL_CMAKE_RIDBUS}" PATH)
