# -*- lua -*-

require 'libpaths'

local assert = assert

module('paths')


install_prefix = [[C:/Program Files/Torch]]
install_bin_subdir = [[bin]]
install_man_subdir = [[share/man]]
install_lib_subdir = [[lib]]
install_share_subdir = [[share]]
install_include_subdir = [[include]]
install_hlp_subdir = [[share/torch/hlp]]
install_html_subdir = [[share/torch/html]]
install_cmake_subdir = [[share/torch/cmake]]
install_lua_path_subdir = [[share/lua/5.1]]
install_lua_cpath_subdir = [[lib/lua/5.1]]
install_bin_ridbus = [[..]]
install_cmake_ridbus = [[../../..]]

local e = execdir()
if e ~= nil then
   install_prefix = concat(e,install_bin_ridbus)
end

install_bin = concat(install_prefix, install_bin_subdir)
install_man = concat(install_prefix, install_man_subdir)
install_lib = concat(install_prefix, install_lib_subdir)
install_share = concat(install_prefix, install_share_subdir)
install_include = concat(install_prefix, install_include_subdir)
install_hlp = concat(install_prefix, install_hlp_subdir)
install_html = concat(install_prefix, install_html_subdir)
install_cmake = concat(install_prefix, install_cmake_subdir)
install_lua_path = concat(install_prefix, install_lua_path_subdir)
install_lua_cpath = concat(install_prefix, install_lua_cpath_subdir)

assert(concat(install_bin,install_bin_ridbus) == install_prefix)
assert(concat(install_cmake,install_cmake_ridbus) == install_prefix)


function files(s)
   local d = dir(s)
   local n = 0
   return function()
             n = n + 1
             if (d and n <= #d) then
                return d[n]
             else
                return nil
             end
          end
end
